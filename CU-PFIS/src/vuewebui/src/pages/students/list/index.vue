<script setup>
import { computed, onMounted, ref, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { useRouter } from 'vue-router'
import DataTable from 'datatables.net-vue3'
import DataTablesCore from 'datatables.net-dt'
import { useAuthStore } from '@/stores/auth'
import { useStudentsStore } from '@/stores/students'
import { useReferenceDataStore } from '@/stores/referenceData'
import { useLocalizedField } from '@/composables/useLocalizedField'
import { usePdfPrint } from '@/composables/usePdfPrint'
import '@/styles/datatables.css'

DataTable.use(DataTablesCore)

const { t } = useI18n()
const router = useRouter()
const authStore = useAuthStore()
const studentsStore = useStudentsStore()
const refStore = useReferenceDataStore()
const { locale, pick, formatFullName } = useLocalizedField()
const { printCurrentView } = usePdfPrint()

const dtTableRef = ref(null)
const selectedCohort = ref(null)
const selectedMajor = ref(null)
const selectedParentRound = ref(null)

const cohortOptions = computed(() => [
  { value: null, title: t('common.all') },
  ...refStore.academicYears.map((y) => ({
    value: y.yearBe,
    title: locale.value === 'en' ? y.labelEn : y.labelTh,
  })),
])

const majorOptions = computed(() => [
  { value: null, title: t('common.all') },
  ...refStore.majors.map((m) => ({
    value: m.externalId,
    title: locale.value === 'en' ? m.nameEn : m.nameTh,
  })),
])

const parentRoundOptions = computed(() => [
  { value: null, title: t('common.all') },
  ...refStore.getParentAdmissionRounds().map((r) => ({
    value: r.externalId,
    title: locale.value === 'en' ? r.nameEn : r.nameTh,
  })),
])

function admissionLabel(item) {
  if (item.admissionRoundExternalId && refStore.admissionRounds.length > 0) {
    return refStore.getAdmissionRoundLabel(item.admissionRoundExternalId, locale.value)
  }
  return pick(item.admissionRoundNameTh, item.admissionRoundNameEn) || '-'
}

const tableRows = computed(() =>
  studentsStore.items.map((item) => {
    const fullName = formatFullName(item)
    const majorName = pick(item.majorNameTh, item.majorNameEn)
    const admission = admissionLabel(item)
    const statusLabel = item.isStaffVerifiedEdit
      ? t('common.staffVerified')
      : t('common.unverifiedSync')

    return {
      externalId: item.externalId,
      studentCode: item.studentCode,
      studentCodeSearch: `${item.studentCode} ${item.sourceSystem || ''}`.trim(),
      sourceSystem: item.sourceSystem,
      fullName,
      fullNameSearch: [
        item.firstNameTh,
        item.lastNameTh,
        item.firstNameEn,
        item.lastNameEn,
        `${t('student.studyYear')} ${item.studyYear}`,
      ]
        .filter(Boolean)
        .join(' '),
      studyYear: item.studyYear,
      cohortYearBe: item.cohortYearBe,
      majorName,
      majorSearch: [item.majorCode, item.majorNameTh, item.majorNameEn].filter(Boolean).join(' '),
      admissionLabel: admission,
      admissionSearch: [admission, item.admissionRoundNameTh, item.admissionRoundNameEn]
        .filter(Boolean)
        .join(' '),
      isStaffVerifiedEdit: item.isStaffVerifiedEdit,
      statusLabel,
    }
  }),
)

const columns = computed(() => [
  {
    data: null,
    title: t('student.studentCode'),
    render: {
      display: '#studentCode',
      sort: 'studentCode',
      filter: 'studentCodeSearch',
      type: 'studentCode',
      _: 'studentCode',
    },
  },
  {
    data: null,
    title: t('student.fullName'),
    render: {
      display: '#fullName',
      sort: 'fullName',
      filter: 'fullNameSearch',
      type: 'fullName',
      _: 'fullName',
    },
  },
  {
    data: 'cohortYearBe',
    title: t('student.cohortYear'),
    className: 'dt-center',
  },
  {
    data: null,
    title: t('common.major'),
    render: {
      display: 'majorName',
      sort: 'majorName',
      filter: 'majorSearch',
      type: 'majorName',
      _: 'majorName',
    },
  },
  {
    data: null,
    title: t('student.admissionRound'),
    render: {
      display: 'admissionLabel',
      sort: 'admissionLabel',
      filter: 'admissionSearch',
      type: 'admissionLabel',
      _: 'admissionLabel',
    },
  },
  {
    data: null,
    title: t('common.status'),
    className: 'dt-center',
    render: {
      display: '#status',
      sort: 'statusLabel',
      filter: 'statusLabel',
      type: 'statusLabel',
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
  order: [
    [2, 'desc'],
    [0, 'asc'],
  ],
  language: {
    search: t('datatable.search'),
    searchPlaceholder: t('datatable.studentSearchPlaceholder'),
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
  await studentsStore.fetchList({
    cohortYearBe: selectedCohort.value || undefined,
    majorExternalId: selectedMajor.value || undefined,
    admissionRoundExternalId: selectedParentRound.value || undefined,
  })
}

onMounted(async () => {
  await Promise.all([refStore.ensureLoaded(), loadList()])
})

watch([() => authStore.personaKey, selectedCohort, selectedMajor, selectedParentRound], loadList)

function resetFilters() {
  selectedCohort.value = null
  selectedMajor.value = null
  selectedParentRound.value = null
  dtTableRef.value?.dt?.search('').draw()
}

function goToView(externalId) {
  router.push(`/students/view/${externalId}`)
}

function goToEdit(externalId) {
  router.push(`/students/edit/${externalId}`)
}
</script>

<template>
  <div>
    <div class="cu-page-header">
      <div>
        <h1 class="text-h5 font-weight-bold">{{ t('student.title') }}</h1>
        <p class="text-body-2 cu-muted mb-0">{{ t('student.subtitle') }}</p>
      </div>

      <div class="d-flex flex-wrap ga-2 no-print">
        <v-btn
          color="secondary"
          prepend-icon="mdi-cloud-sync-outline"
          to="/students/sync"
          data-testid="students-sync-btn"
        >
          {{ t('student.syncFromCuppep') }}
        </v-btn>
        <v-btn variant="outlined" prepend-icon="mdi-file-pdf-box" @click="printCurrentView">
          {{ t('common.exportAllPdf') }}
        </v-btn>
      </div>
    </div>

    <!-- A02 CUPPEP Sync Info Banner -->
    <v-alert
      type="info"
      variant="tonal"
      density="compact"
      class="mb-4"
      icon="mdi-sync"
    >
      {{ t('student.cuppepSyncBanner') }}
    </v-alert>

    <!-- Department Scope Banner (A03) -->
    <v-alert
      v-if="authStore.isStaff"
      type="warning"
      variant="tonal"
      density="compact"
      class="mb-4"
      icon="mdi-office-building-outline"
    >
      {{ t('common.departmentScopeNotice') }} —
      <strong>{{ pick(authStore.currentUser.departmentNameTh, authStore.currentUser.departmentNameEn) }}</strong>
    </v-alert>

    <v-alert v-if="studentsStore.error" type="error" variant="tonal" density="compact" class="mb-4">
      {{ studentsStore.error }}
    </v-alert>

    <!-- Structured Filters -->
    <v-card class="pa-4 mb-5 no-print">
      <v-row align="center">
        <v-col cols="12" md="3">
          <v-select
            v-model="selectedCohort"
            :items="cohortOptions"
            :label="t('student.cohortYear')"
          />
        </v-col>
        <v-col cols="12" md="4">
          <v-select
            v-model="selectedMajor"
            :items="majorOptions"
            :label="t('common.major')"
          />
        </v-col>
        <v-col cols="12" md="3">
          <v-select
            v-model="selectedParentRound"
            :items="parentRoundOptions"
            :label="t('student.admissionRound')"
          />
        </v-col>
        <v-col cols="12" md="2">
          <v-btn variant="outlined" color="secondary" block @click="resetFilters">
            {{ t('common.reset') }}
          </v-btn>
        </v-col>
      </v-row>
    </v-card>

    <!-- DataTables.net Table -->
    <v-card>
      <v-progress-linear v-if="studentsStore.loading" indeterminate color="secondary" />
      <div class="cu-datatable-wrap">
        <DataTable
          :key="locale"
          ref="dtTableRef"
          id="students-datatable"
          :data="tableRows"
          :columns="columns"
          :options="dtOptions"
          class="display hover row-border"
          data-testid="students-table"
        >
          <template #studentCode="{ rowData }">
            <div class="font-weight-bold">{{ rowData.studentCode }}</div>
            <span class="cu-dt-chip cu-dt-chip--xs cu-dt-chip--secondary mt-1">
              {{ rowData.sourceSystem }}
            </span>
          </template>

          <template #fullName="{ rowData }">
            <div class="font-weight-bold">{{ rowData.fullName }}</div>
            <div class="text-caption cu-muted">
              {{ t('student.studyYear') }} {{ rowData.studyYear }}
            </div>
          </template>

          <template #status="{ rowData }">
            <span
              class="cu-dt-chip"
              :class="rowData.isStaffVerifiedEdit ? 'cu-dt-chip--success' : 'cu-dt-chip--info'"
            >
              <i
                class="mdi"
                :class="rowData.isStaffVerifiedEdit ? 'mdi-shield-check' : 'mdi-sync'"
                aria-hidden="true"
              />
              {{ rowData.statusLabel }}
            </span>
          </template>

          <template #actions="{ rowData }">
            <div class="cu-dt-actions">
              <button
                type="button"
                class="cu-dt-btn cu-dt-btn--tonal-secondary"
                :data-testid="`student-view-${rowData.studentCode}`"
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
  </div>
</template>
