<script setup>
import { computed, onMounted, ref, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { useAuthStore } from '@/stores/auth'
import { useStudentsStore } from '@/stores/students'
import { useReferenceDataStore } from '@/stores/referenceData'
import { useLocalizedField } from '@/composables/useLocalizedField'
import { usePdfPrint } from '@/composables/usePdfPrint'

const { t } = useI18n()
const authStore = useAuthStore()
const studentsStore = useStudentsStore()
const refStore = useReferenceDataStore()
const { locale, pick, formatFullName } = useLocalizedField()
const { printCurrentView } = usePdfPrint()

const search = ref('')
const selectedCohort = ref(null)
const selectedMajor = ref(null)
const selectedParentRound = ref(null)
let searchTimer = null

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

async function loadList() {
  await studentsStore.fetchList({
    search: search.value.trim() || undefined,
    cohortYearBe: selectedCohort.value || undefined,
    majorExternalId: selectedMajor.value || undefined,
    admissionRoundExternalId: selectedParentRound.value || undefined,
  })
}

onMounted(async () => {
  await Promise.all([refStore.ensureLoaded(), loadList()])
})

watch([() => authStore.personaKey, selectedCohort, selectedMajor, selectedParentRound], loadList)

watch(search, () => {
  clearTimeout(searchTimer)
  searchTimer = setTimeout(loadList, 250)
})

function admissionLabel(item) {
  if (item.admissionRoundExternalId && refStore.admissionRounds.length > 0) {
    return refStore.getAdmissionRoundLabel(item.admissionRoundExternalId, locale.value)
  }
  return pick(item.admissionRoundNameTh, item.admissionRoundNameEn) || '-'
}

function resetFilters() {
  search.value = ''
  selectedCohort.value = null
  selectedMajor.value = null
  selectedParentRound.value = null
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

    <!-- Filters -->
    <v-card class="pa-4 mb-5 no-print">
      <v-row align="center">
        <v-col cols="12" md="3">
          <v-text-field
            v-model="search"
            :label="t('common.search')"
            prepend-inner-icon="mdi-magnify"
            clearable
            data-testid="students-search-input"
          />
        </v-col>
        <v-col cols="12" md="2">
          <v-select
            v-model="selectedCohort"
            :items="cohortOptions"
            :label="t('student.cohortYear')"
          />
        </v-col>
        <v-col cols="12" md="3">
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
        <v-col cols="12" md="1">
          <v-btn variant="text" block @click="resetFilters">
            {{ t('common.reset') }}
          </v-btn>
        </v-col>
      </v-row>
    </v-card>

    <!-- Data Table -->
    <v-card>
      <v-progress-linear v-if="studentsStore.loading" indeterminate color="secondary" />
      <v-table hover data-testid="students-table">
        <thead>
          <tr>
            <th>{{ t('student.studentCode') }}</th>
            <th>{{ t('student.fullName') }}</th>
            <th class="text-center">{{ t('student.cohortYear') }}</th>
            <th>{{ t('common.major') }}</th>
            <th>{{ t('student.admissionRound') }}</th>
            <th class="text-center">{{ t('common.status') }}</th>
            <th class="text-right no-print">{{ t('common.actions') }}</th>
          </tr>
        </thead>
        <tbody>
          <tr v-if="!studentsStore.loading && studentsStore.items.length === 0">
            <td colspan="7" class="text-center py-8 cu-muted">{{ t('common.noData') }}</td>
          </tr>
          <tr v-for="item in studentsStore.items" :key="item.externalId">
            <td>
              <div class="font-weight-bold">{{ item.studentCode }}</div>
              <v-chip size="x-small" color="secondary" variant="tonal" class="mt-1">
                {{ item.sourceSystem }}
              </v-chip>
            </td>
            <td>
              <div class="font-weight-bold">{{ formatFullName(item) }}</div>
              <div class="text-caption cu-muted">
                {{ t('student.studyYear') }} {{ item.studyYear }}
              </div>
            </td>
            <td class="text-center">{{ item.cohortYearBe }}</td>
            <td>{{ pick(item.majorNameTh, item.majorNameEn) }}</td>
            <td>{{ admissionLabel(item) }}</td>
            <td class="text-center">
              <v-chip
                size="small"
                :color="item.isStaffVerifiedEdit ? 'success' : 'info'"
                variant="tonal"
              >
                <v-icon
                  start
                  :icon="item.isStaffVerifiedEdit ? 'mdi-shield-check' : 'mdi-sync'"
                  size="14"
                />
                {{ item.isStaffVerifiedEdit ? t('common.staffVerified') : t('common.unverifiedSync') }}
              </v-chip>
            </td>
            <td class="text-right no-print">
              <v-btn
                size="small"
                variant="tonal"
                color="secondary"
                class="mr-2"
                :to="`/students/view/${item.externalId}`"
                :data-testid="`student-view-${item.studentCode}`"
              >
                {{ t('common.view') }}
              </v-btn>
              <v-btn
                size="small"
                variant="outlined"
                :to="`/students/edit/${item.externalId}`"
              >
                {{ t('common.edit') }}
              </v-btn>
            </td>
          </tr>
        </tbody>
      </v-table>
    </v-card>
  </div>
</template>
