<script setup>
import { computed, onMounted, ref, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { useRouter } from 'vue-router'
import { useAbility } from '@casl/vue'
import DataTable from 'datatables.net-vue3'
import DataTablesCore from 'datatables.net-dt'
import { useAuthStore } from '@/stores/auth'
import { useTrainingsStore } from '@/stores/trainings'
import { useLocalizedField } from '@/composables/useLocalizedField'
import '@/styles/datatables.css'

DataTable.use(DataTablesCore)

const { t } = useI18n()
const router = useRouter()
const { can } = useAbility()
const authStore = useAuthStore()
const trainingsStore = useTrainingsStore()
const { locale, pick } = useLocalizedField()

const dtTableRef = ref(null)
const selectedFiscalYear = ref(2570)
const selectedCategory = ref(null)
const selectedStatus = ref(null)
const csvInputRef = ref(null)
const snackbar = ref(false)
const snackbarText = ref('')
const snackbarColor = ref('success')

const canImportHistorical = computed(() => can('approve', 'training-requests'))
const canCreate = computed(
  () => can('create-own', 'training-requests') || can('view-all', 'training-requests'),
)

const fiscalYearOptions = computed(() => {
  const fromPolicies = trainingsStore.policies.map((p) => p.fiscalYearBe)
  const years = Array.from(new Set([2568, 2569, 2570, 2571, ...fromPolicies])).sort((a, b) => b - a)
  return [
    { value: null, title: t('common.all') },
    ...years.map((fy) => ({ value: fy, title: `${t('training.fiscalYear')} ${fy}` })),
  ]
})

const categoryOptions = computed(() => [
  { value: null, title: t('common.all') },
  { value: 'AcademicFunded', title: t('training.category.AcademicFunded') },
  { value: 'NonAcademicFunded', title: t('training.category.NonAcademicFunded') },
  { value: 'FacultyOrganized', title: t('training.category.FacultyOrganized') },
  { value: 'ShortTermOrOverseas', title: t('training.category.ShortTermOrOverseas') },
])

const statusOptions = computed(() => [
  { value: null, title: t('common.all') },
  { value: 'Draft', title: t('training.status.Draft') },
  { value: 'Submitted', title: t('training.status.Submitted') },
  { value: 'RevisionRequested', title: t('training.status.RevisionRequested') },
  { value: 'VerifiedInProgress', title: t('training.status.VerifiedInProgress') },
  { value: 'Approved', title: t('training.status.Approved') },
  { value: 'Cancelled', title: t('training.status.Cancelled') },
])

function formatMoney(val) {
  return Number(val || 0).toLocaleString('en-US', {
    minimumFractionDigits: 2,
    maximumFractionDigits: 2,
  })
}

function statusChipClass(status) {
  const map = {
    Draft: 'cu-dt-chip--default',
    Submitted: 'cu-dt-chip--info',
    RevisionRequested: 'cu-dt-chip--warning',
    VerifiedInProgress: 'cu-dt-chip--secondary',
    Approved: 'cu-dt-chip--success',
    Cancelled: 'cu-dt-chip--error',
  }
  return map[status] || 'cu-dt-chip--default'
}

const tableRows = computed(() =>
  trainingsStore.items.map((item) => ({
    externalId: item.externalId,
    facultyName: pick(item.facultyNameTh, item.facultyNameEn),
    departmentName: pick(item.departmentNameTh, item.departmentNameEn),
    title: pick(item.titleTh, item.titleEn),
    organizer: pick(item.organizerAndVenueTh, item.organizerAndVenueEn),
    categoryLabel: t(`training.category.${item.category}`),
    workTypeLabel: t(`training.workType.${item.workType}`),
    dateRange: `${item.startDate} – ${item.endDate}`,
    amount: item.status === 'Approved' ? item.approvedAmount : item.requestedAmount,
    status: item.status,
    statusLabel: t(`training.status.${item.status}`),
    searchBlob: [
      item.personnelCode,
      item.facultyNameTh,
      item.facultyNameEn,
      item.titleTh,
      item.titleEn,
      item.organizerAndVenueTh,
    ]
      .filter(Boolean)
      .join(' '),
  })),
)

const columns = computed(() => [
  {
    data: null,
    title: t('faculty.trainingTitle'),
    render: {
      display: '#title',
      sort: 'title',
      filter: 'searchBlob',
      type: 'title',
      _: 'title',
    },
  },
  {
    data: null,
    title: t('faculty.fullName'),
    render: {
      display: '#faculty',
      sort: 'facultyName',
      filter: 'searchBlob',
      type: 'facultyName',
      _: 'facultyName',
    },
  },
  {
    data: 'categoryLabel',
    title: t('training.categoryLabel'),
  },
  {
    data: 'dateRange',
    title: t('faculty.trainingDate'),
    className: 'dt-nowrap',
  },
  {
    data: null,
    title: t('training.requestedAmount'),
    type: 'num',
    className: 'dt-right dt-nowrap',
    render: {
      display: '#amount',
      sort: 'amount',
      _: 'amount',
    },
  },
  {
    data: null,
    title: t('training.statusLabel'),
    className: 'dt-center',
    render: {
      display: '#status',
      sort: 'statusLabel',
      filter: 'statusLabel',
      _: 'statusLabel',
    },
  },
  {
    data: null,
    title: t('common.actions'),
    orderable: false,
    searchable: false,
    className: 'dt-right dt-nowrap no-print',
    render: {
      display: '#actions',
      _: () => '',
    },
  },
])

const dtOptions = computed(() => ({
  autoWidth: false,
  pageLength: 10,
  lengthMenu: [
    [10, 25, 50, 100, -1],
    [10, 25, 50, 100, t('common.all')],
  ],
  order: [[3, 'desc']],
  language: {
    search: t('datatable.search'),
    searchPlaceholder: t('datatable.searchPlaceholder'),
    lengthMenu: t('datatable.lengthMenu'),
    info: t('datatable.info'),
    infoEmpty: t('datatable.infoEmpty'),
    infoFiltered: t('datatable.infoFiltered'),
    emptyTable: t('common.noData'),
    zeroRecords: t('datatable.zeroRecords'),
    paginate: {
      first: '«',
      previous: '‹',
      next: '›',
      last: '»',
    },
  },
}))

async function loadList() {
  await Promise.all([
    trainingsStore.fetchPolicies(),
    trainingsStore.fetchList({
      fiscalYearBe: selectedFiscalYear.value || undefined,
      category: selectedCategory.value || undefined,
      status: selectedStatus.value || undefined,
    }),
  ])
}

onMounted(loadList)
watch([() => authStore.personaKey, selectedFiscalYear, selectedCategory, selectedStatus], loadList)

function resetFilters() {
  selectedFiscalYear.value = 2570
  selectedCategory.value = null
  selectedStatus.value = null
  dtTableRef.value?.dt?.search('').draw()
}

function goToView(externalId) {
  router.push(`/trainings/view/${externalId}`)
}

function goToEdit(externalId) {
  router.push(`/trainings/edit/${externalId}`)
}

function triggerHistoricalCsvUpload() {
  csvInputRef.value?.click()
}

// Historical Excel/CSV Migration (A04):
// Columns: SourceRecordId,PersonnelCode,Category,WorkType,TitleTh,TitleEn,OrganizerTh,StartDate,EndDate,ApprovedAmount
function handleHistoricalCsvFile(event) {
  const file = event.target.files?.[0]
  if (!file) return

  const reader = new FileReader()
  reader.onload = async () => {
    const lines = String(reader.result || '')
      .split(/\r?\n/)
      .filter((l) => l.trim())
    const parsed = []
    for (let i = 1; i < lines.length; i += 1) {
      const cols = lines[i].split(',').map((c) => c.replace(/^"|"$/g, '').trim())
      if (!cols[0] || !cols[1]) continue
      parsed.push({
        sourceRecordId: cols[0],
        personnelCode: cols[1],
        category: cols[2] || 'AcademicFunded',
        workType: cols[3] || 'Academic',
        titleTh: cols[4] || 'Historical Training',
        titleEn: cols[5] || null,
        organizerAndVenueTh: cols[6] || '-',
        organizerAndVenueEn: null,
        startDate: cols[7],
        endDate: cols[8] || cols[7],
        approvedAmount: Number(cols[9]) || 0,
      })
    }

    try {
      const result = await trainingsStore.importHistorical(parsed, true)
      snackbarColor.value = result.failedCount > 0 ? 'warning' : 'success'
      snackbarText.value = t('training.importHistoricalDone', {
        imported: result.importedCount,
        skipped: result.skippedDuplicateCount,
        failed: result.failedCount,
      })
      snackbar.value = true
      await loadList()
    } catch (err) {
      snackbarColor.value = 'error'
      snackbarText.value = err.message || t('common.apiError')
      snackbar.value = true
    } finally {
      event.target.value = ''
    }
  }
  reader.readAsText(file)
}
</script>

<template>
  <div>
    <div class="cu-page-header">
      <div>
        <h1 class="text-h5 font-weight-bold">{{ t('training.tabRequests') }}</h1>
        <p class="text-body-2 cu-muted mb-0">{{ t('training.subtitle') }}</p>
      </div>

      <div class="d-flex flex-wrap ga-2 no-print">
        <input
          ref="csvInputRef"
          type="file"
          accept=".csv"
          class="d-none"
          @change="handleHistoricalCsvFile"
        />
        <v-btn
          v-if="canImportHistorical"
          variant="outlined"
          color="secondary"
          prepend-icon="mdi-file-excel-outline"
          data-testid="training-import-csv-btn"
          @click="triggerHistoricalCsvUpload"
        >
          {{ t('training.importHistorical') }}
        </v-btn>
        <v-btn
          v-if="canCreate"
          color="primary"
          prepend-icon="mdi-plus"
          to="/trainings/create"
          data-testid="training-create-btn"
        >
          {{ t('common.create') }}
        </v-btn>
      </div>
    </div>

    <v-alert
      v-if="authStore.isStaff"
      type="info"
      variant="tonal"
      density="compact"
      class="mb-4"
      icon="mdi-office-building-outline"
    >
      {{ t('common.departmentScopeNotice') }} —
      <strong>{{ pick(authStore.currentUser.departmentNameTh, authStore.currentUser.departmentNameEn) }}</strong>
    </v-alert>

    <v-alert v-if="trainingsStore.error" type="error" variant="tonal" density="compact" class="mb-4">
      {{ trainingsStore.error }}
    </v-alert>

    <!-- Filters Bar -->
    <v-card class="pa-4 mb-5 no-print">
      <v-row align="center">
        <v-col cols="12" md="3">
          <v-select
            v-model="selectedFiscalYear"
            :items="fiscalYearOptions"
            :label="t('training.fiscalYear')"
            hide-details
            data-testid="training-fy-select"
          />
        </v-col>
        <v-col cols="12" md="4">
          <v-select
            v-model="selectedCategory"
            :items="categoryOptions"
            :label="t('training.categoryLabel')"
            hide-details
          />
        </v-col>
        <v-col cols="12" md="3">
          <v-select
            v-model="selectedStatus"
            :items="statusOptions"
            :label="t('training.statusLabel')"
            hide-details
          />
        </v-col>
        <v-col cols="12" md="2">
          <v-btn variant="outlined" color="primary" block @click="resetFilters">
            {{ t('common.reset') }}
          </v-btn>
        </v-col>
      </v-row>
    </v-card>

    <!-- DataTable Card -->
    <v-card>
      <v-progress-linear v-if="trainingsStore.loading" indeterminate color="primary" />
      <div class="cu-datatable-wrap">
        <DataTable
          :key="locale"
          ref="dtTableRef"
          id="trainings-datatable"
          :data="tableRows"
          :columns="columns"
          :options="dtOptions"
          class="display hover row-border"
          data-testid="trainings-table"
        >
          <template #title="{ rowData }">
            <div class="font-weight-bold">{{ rowData.title }}</div>
            <div class="text-caption cu-muted">{{ rowData.organizer }} · {{ rowData.workTypeLabel }}</div>
          </template>

          <template #faculty="{ rowData }">
            <div class="font-weight-medium">{{ rowData.facultyName }}</div>
            <div class="text-caption cu-muted">{{ rowData.departmentName }}</div>
          </template>

          <template #amount="{ rowData }">
            <span class="font-weight-medium">{{ formatMoney(rowData.amount) }}</span>
          </template>

          <template #status="{ rowData }">
            <span class="cu-dt-chip" :class="statusChipClass(rowData.status)">
              {{ rowData.statusLabel }}
            </span>
          </template>

          <template #actions="{ rowData }">
            <div class="cu-dt-actions">
              <button
                type="button"
                class="cu-dt-btn cu-dt-btn--tonal-primary"
                :data-testid="`training-view-${rowData.externalId}`"
                @click="goToView(rowData.externalId)"
              >
                {{ t('common.view') }}
              </button>
              <button
                v-if="rowData.status === 'Draft' || rowData.status === 'RevisionRequested'"
                type="button"
                class="cu-dt-btn cu-dt-btn--outlined"
                @click="goToEdit(rowData.externalId)"
              >
                {{ t('common.edit') }}
              </button>
            </div>
          </template>
        </DataTable>
      </div>
    </v-card>

    <v-snackbar v-model="snackbar" :color="snackbarColor" timeout="4500">
      {{ snackbarText }}
    </v-snackbar>
  </div>
</template>
