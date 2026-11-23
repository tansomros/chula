<script setup>
import { computed, onMounted, ref, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { useRouter } from 'vue-router'
import { useAbility } from '@casl/vue'
import DataTable from 'datatables.net-vue3'
import DataTablesCore from 'datatables.net-dt'
import { useAuthStore } from '@/stores/auth'
import { useFacultyMembersStore } from '@/stores/facultyMembers'
import { useReferenceDataStore } from '@/stores/referenceData'
import { useLocalizedField } from '@/composables/useLocalizedField'
import { usePdfPrint } from '@/composables/usePdfPrint'
import '@/styles/datatables.css'

DataTable.use(DataTablesCore)

const { t } = useI18n()
const router = useRouter()
const { can } = useAbility()
const authStore = useAuthStore()
const facultyStore = useFacultyMembersStore()
const refStore = useReferenceDataStore()
const { locale, pick, formatFullName } = useLocalizedField()
const { printCurrentView } = usePdfPrint()

const dtTableRef = ref(null)
const selectedDepartment = ref(null)
const selectedPosition = ref(null)
const snackbar = ref(false)
const snackbarMessage = ref('')

const canCreateOrNotify = computed(() => can('edit-all', 'faculty'))
const canExportAll = computed(() => can('export-all', 'faculty'))

const departmentOptions = computed(() => [
  { value: null, title: t('common.all') },
  ...refStore.departments.map((d) => ({
    value: d.externalId,
    title: locale.value === 'en' ? d.nameEn : d.nameTh,
  })),
])

const positionOptions = computed(() => [
  { value: null, title: t('common.all') },
  ...refStore.academicPositions.map((p) => ({
    value: p.externalId,
    title: locale.value === 'en' ? p.titleEn : p.titleTh,
  })),
])

const tableRows = computed(() =>
  facultyStore.items.map((item) => {
    const fullName = formatFullName(item, true)
    const academicPosition = pick(item.academicPositionTitleTh, item.academicPositionTitleEn)
    const departmentName = pick(item.departmentNameTh, item.departmentNameEn)
    const publicationsCount = item.publicationsCount ?? 0

    return {
      externalId: item.externalId,
      personnelCode: item.personnelCode,
      fullName,
      email: item.email || '',
      fullNameSearch: [
        item.professionalTitleTh,
        item.firstNameTh,
        item.lastNameTh,
        item.professionalTitleEn,
        item.firstNameEn,
        item.lastNameEn,
        item.email,
      ]
        .filter(Boolean)
        .join(' '),
      academicPosition,
      academicPositionSearch: [item.academicPositionTitleTh, item.academicPositionTitleEn]
        .filter(Boolean)
        .join(' '),
      departmentName,
      departmentSearch: [item.departmentNameTh, item.departmentNameEn].filter(Boolean).join(' '),
      publicationsCount,
      approvedTrainingsCount: item.approvedTrainingsCount ?? 0,
    }
  }),
)

const columns = computed(() => [
  {
    data: 'personnelCode',
    title: t('faculty.personnelCode'),
    className: 'font-weight-medium dt-nowrap',
  },
  {
    data: null,
    title: t('faculty.fullName'),
    render: {
      display: '#fullName',
      sort: 'fullName',
      filter: 'fullNameSearch',
      type: 'fullName',
      _: 'fullName',
    },
  },
  {
    data: null,
    title: t('faculty.academicPosition'),
    render: {
      display: 'academicPosition',
      sort: 'academicPosition',
      filter: 'academicPositionSearch',
      type: 'academicPosition',
      _: 'academicPosition',
    },
  },
  {
    data: null,
    title: t('common.department'),
    render: {
      display: 'departmentName',
      sort: 'departmentName',
      filter: 'departmentSearch',
      type: 'departmentName',
      _: 'departmentName',
    },
  },
  {
    data: null,
    title: t('faculty.publicationsCount'),
    type: 'num',
    className: 'dt-center',
    render: {
      display: '#publications',
      sort: 'publicationsCount',
      filter: 'publicationsCount',
      type: 'publicationsCount',
      _: 'publicationsCount',
    },
  },
  {
    data: null,
    title: t('faculty.trainingsCount'),
    type: 'num',
    className: 'dt-center',
    render: {
      display: '#trainings',
      sort: 'approvedTrainingsCount',
      filter: 'approvedTrainingsCount',
      type: 'approvedTrainingsCount',
      _: 'approvedTrainingsCount',
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
  order: [[0, 'asc']],
  language: {
    search: t('datatable.search'),
    searchPlaceholder: t('datatable.facultySearchPlaceholder'),
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
  await facultyStore.fetchList({
    departmentExternalId: selectedDepartment.value || undefined,
    academicPositionExternalId: selectedPosition.value || undefined,
  })
}

onMounted(async () => {
  await Promise.all([refStore.ensureLoaded(), loadList()])
})

watch([() => authStore.personaKey, selectedDepartment, selectedPosition], loadList)

function resetFilters() {
  selectedDepartment.value = null
  selectedPosition.value = null
  dtTableRef.value?.dt?.search('').draw()
}

function handleNotifyAll() {
  snackbarMessage.value = t('faculty.notifySent')
  snackbar.value = true
}

function goToView(externalId) {
  router.push(`/faculty-members/view/${externalId}`)
}

function goToEdit(externalId) {
  router.push(`/faculty-members/edit/${externalId}`)
}
</script>

<template>
  <div>
    <div class="cu-page-header">
      <div>
        <h1 class="text-h5 font-weight-bold">{{ t('faculty.title') }}</h1>
        <p class="text-body-2 cu-muted mb-0">{{ t('faculty.subtitle') }}</p>
      </div>

      <div class="d-flex flex-wrap ga-2 no-print">
        <v-btn
          v-if="canCreateOrNotify"
          variant="outlined"
          color="secondary"
          prepend-icon="mdi-email-fast-outline"
          data-testid="faculty-notify-btn"
          @click="handleNotifyAll"
        >
          {{ t('faculty.notifyUpdate') }}
        </v-btn>
        <v-btn
          v-if="canExportAll"
          variant="outlined"
          prepend-icon="mdi-file-pdf-box"
          @click="printCurrentView"
        >
          {{ t('common.exportAllPdf') }}
        </v-btn>
        <v-btn
          v-if="canCreateOrNotify"
          color="primary"
          prepend-icon="mdi-plus"
          to="/faculty-members/create"
          data-testid="faculty-create-btn"
        >
          {{ t('common.create') }}
        </v-btn>
      </div>
    </div>

    <!-- Role / Department scope banner (A03) -->
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

    <v-alert
      v-if="authStore.isFaculty"
      type="info"
      variant="tonal"
      density="compact"
      class="mb-4"
      icon="mdi-account-lock-outline"
    >
      {{ t('common.ownProfileOnlyNotice') }}
    </v-alert>

    <v-alert v-if="facultyStore.error" type="error" variant="tonal" density="compact" class="mb-4">
      {{ facultyStore.error }}
    </v-alert>

    <!-- Structured Filters -->
    <v-card class="pa-4 mb-5 no-print">
      <v-row align="center">
        <v-col cols="12" md="5">
          <v-select
            v-model="selectedDepartment"
            :items="departmentOptions"
            :label="t('common.department')"
            :disabled="authStore.isStaff || authStore.isFaculty"
          />
        </v-col>
        <v-col cols="12" md="5">
          <v-select
            v-model="selectedPosition"
            :items="positionOptions"
            :label="t('faculty.academicPosition')"
          />
        </v-col>
        <v-col cols="12" md="2">
          <v-btn variant="outlined" color="primary" block @click="resetFilters">
            {{ t('common.reset') }}
          </v-btn>
        </v-col>
      </v-row>
    </v-card>

    <!-- DataTables.net Table -->
    <v-card>
      <v-progress-linear v-if="facultyStore.loading" indeterminate color="primary" />
      <div class="cu-datatable-wrap">
        <DataTable
          :key="locale"
          ref="dtTableRef"
          id="faculty-datatable"
          :data="tableRows"
          :columns="columns"
          :options="dtOptions"
          class="display hover row-border"
          data-testid="faculty-table"
        >
          <template #fullName="{ rowData }">
            <div class="font-weight-bold">{{ rowData.fullName }}</div>
            <div class="text-caption cu-muted">{{ rowData.email }}</div>
          </template>

          <template #publications="{ rowData }">
            <span class="cu-dt-chip cu-dt-chip--primary">
              {{ rowData.publicationsCount }}
            </span>
          </template>

          <template #trainings="{ rowData }">
            <span class="cu-dt-chip cu-dt-chip--secondary">
              {{ rowData.approvedTrainingsCount }}
            </span>
          </template>

          <template #actions="{ rowData }">
            <div class="cu-dt-actions">
              <button
                type="button"
                class="cu-dt-btn cu-dt-btn--tonal-primary"
                :data-testid="`faculty-view-${rowData.personnelCode}`"
                @click="goToView(rowData.externalId)"
              >
                {{ t('common.view') }}
              </button>
              <button
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

    <v-snackbar v-model="snackbar" color="success" timeout="3500">
      {{ snackbarMessage }}
    </v-snackbar>
  </div>
</template>
