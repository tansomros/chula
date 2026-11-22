<script setup>
import { computed, onMounted, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { useRoute } from 'vue-router'
import { useAbility } from '@casl/vue'
import { useAuthStore } from '@/stores/auth'
import { useStudentsStore } from '@/stores/students'
import { useReferenceDataStore } from '@/stores/referenceData'
import { useLocalizedField } from '@/composables/useLocalizedField'
import { usePdfPrint } from '@/composables/usePdfPrint'

const { t } = useI18n()
const route = useRoute()
const { can } = useAbility()
const authStore = useAuthStore()
const studentsStore = useStudentsStore()
const refStore = useReferenceDataStore()
const { locale, pick, formatFullName } = useLocalizedField()
const { printCurrentView } = usePdfPrint()

const student = computed(() => studentsStore.currentDetail)
const canViewEmployerEval = computed(() => can('view', 'employer-evaluations'))

const formattedImportedAt = computed(() => {
  if (!student.value?.importedAt) return '-'
  return new Date(student.value.importedAt).toLocaleString()
})

const admissionLabel = computed(() => {
  if (!student.value) return '-'
  if (student.value.admissionRoundExternalId && refStore.admissionRounds.length > 0) {
    return refStore.getAdmissionRoundLabel(student.value.admissionRoundExternalId, locale.value)
  }
  return pick(student.value.admissionRoundNameTh, student.value.admissionRoundNameEn) || '-'
})

async function loadStudent() {
  if (!route.params.id) return
  await Promise.all([refStore.ensureLoaded(), studentsStore.fetchByExternalId(route.params.id)])
}

onMounted(loadStudent)
watch([() => route.params.id, () => authStore.personaKey], loadStudent)
</script>

<template>
  <v-progress-linear v-if="studentsStore.loading" indeterminate color="secondary" class="mb-4" />

  <v-alert v-if="studentsStore.error" type="error" variant="tonal" class="mb-4">
    {{ studentsStore.error }}
  </v-alert>

  <div v-if="student">
    <div class="cu-page-header no-print">
      <div class="d-flex align-center ga-3">
        <v-btn icon="mdi-arrow-left" variant="outlined" to="/students/list" />
        <div>
          <h1 class="text-h5 font-weight-bold">{{ t('student.viewTitle') }}</h1>
          <div class="text-caption cu-muted">
            {{ student.studentCode }} · {{ t('student.lastSynced') }}: {{ formattedImportedAt }}
          </div>
        </div>
      </div>

      <div class="d-flex flex-wrap ga-2">
        <v-btn
          variant="outlined"
          prepend-icon="mdi-printer-outline"
          data-testid="student-export-pdf-btn"
          @click="printCurrentView"
        >
          {{ t('common.printCv') }}
        </v-btn>
        <v-btn
          color="secondary"
          prepend-icon="mdi-pencil-outline"
          :to="`/students/edit/${student.externalId}`"
        >
          {{ t('common.edit') }}
        </v-btn>
      </div>
    </div>

    <!-- Print-ready 11-section Student CV Sheet -->
    <div class="cu-cv-sheet pa-6 pa-md-8">
      <!-- Header -->
      <div class="d-flex flex-wrap justify-space-between align-start pb-5 mb-6 border-b">
        <div>
          <div class="text-caption font-weight-bold text-secondary mb-1">
            {{ t('app.faculty') }} ({{ t('app.acronym') }})
          </div>
          <h2 class="text-h5 font-weight-bold">{{ formatFullName(student) }}</h2>
          <div class="text-body-2 cu-muted mt-1">
            {{ student.studentCode }} · {{ student.firstNameTh }} {{ student.lastNameTh }} /
            {{ student.firstNameEn }} {{ student.lastNameEn }}
          </div>
        </div>
        <div class="d-flex flex-wrap ga-2 mt-3 mt-md-0">
          <v-chip color="secondary" variant="tonal" size="small">
            <v-icon start icon="mdi-sync" size="14" />
            {{ student.sourceSystem }}<template v-if="student.sourceRecordId"> ({{ student.sourceRecordId }})</template>
          </v-chip>
          <v-chip v-if="student.isStaffVerifiedEdit" color="success" variant="tonal" size="small">
            <v-icon start icon="mdi-shield-check-outline" size="14" />
            {{ t('common.staffVerified') }}
          </v-chip>
        </div>
      </div>

      <!-- Sections 1, 2, 3, 4, 6, 7 -->
      <v-row class="mb-6">
        <v-col cols="12" md="6">
          <div class="cu-section-title mb-2">
            <v-icon icon="mdi-card-account-details-outline" color="secondary" />
            {{ t('student.sec1General') }}
          </div>
          <v-sheet border rounded="lg" class="pa-4 mb-4">
            <div class="mb-2">
              <span class="text-caption cu-muted">{{ t('student.cohortYear') }}:</span>
              <strong class="ml-2">{{ student.cohortYearBe }}</strong>
              <span class="mx-2">·</span>
              <span class="text-caption cu-muted">{{ t('student.studyYear') }}:</span>
              <strong class="ml-2">{{ student.studyYear }}</strong>
            </div>
            <div class="mb-2">
              <span class="text-caption cu-muted">{{ t('common.major') }}:</span>
              <strong class="ml-2">{{ pick(student.majorNameTh, student.majorNameEn) }}</strong>
            </div>
            <div v-if="student.departmentNameTh || student.departmentNameEn" class="mb-2">
              <span class="text-caption cu-muted">{{ t('common.department') }}:</span>
              <strong class="ml-2">{{ pick(student.departmentNameTh, student.departmentNameEn) }}</strong>
            </div>
            <div>
              <span class="text-caption cu-muted">{{ t('student.sec6Admission') }}:</span>
              <v-chip size="small" color="primary" variant="tonal" class="ml-2">
                {{ admissionLabel }}
              </v-chip>
            </div>
          </v-sheet>

          <div class="cu-section-title mb-2">
            <v-icon icon="mdi-town-hall" color="secondary" />
            {{ t('student.sec4HighSchool') }}
          </div>
          <v-sheet border rounded="lg" class="pa-4">
            {{ pick(student.highSchoolTh, student.highSchoolEn) || '-' }}
          </v-sheet>
        </v-col>

        <v-col cols="12" md="6">
          <div class="cu-section-title mb-2">
            <v-icon icon="mdi-account-supervisor-outline" color="secondary" />
            {{ t('student.sec2Advisor') }} &amp; {{ t('student.sec3ProjectAdvisor') }}
          </div>
          <v-sheet border rounded="lg" class="pa-4 mb-4">
            <div class="text-caption cu-muted">{{ t('student.sec2Advisor') }}</div>
            <div class="font-weight-medium mb-3">
              {{ pick(student.academicAdvisorNameTh, student.academicAdvisorNameEn) || '-' }}
            </div>
            <div class="text-caption cu-muted">{{ t('student.sec3ProjectAdvisor') }}</div>
            <div class="text-body-2 cu-muted">
              {{ t('student.phase3PendingNote') }}
            </div>
          </v-sheet>

          <!-- Section 7: Senior Project (Phase 4) -->
          <div class="cu-section-title mb-2">
            <v-icon icon="mdi-book-open-page-variant-outline" color="secondary" />
            {{ t('student.sec7Project') }}
          </div>
          <v-sheet border rounded="lg" class="pa-4">
            <div class="text-body-2 cu-muted mb-1">{{ t('student.phase3PendingNote') }}</div>
            <div class="text-caption text-secondary">{{ t('student.projectSsotNote') }}</div>
          </v-sheet>
        </v-col>
      </v-row>

      <!-- Section 5: Internships (CUPPEP) -->
      <div class="mb-6">
        <div class="cu-section-title mb-3">
          <v-icon icon="mdi-hospital-building" color="secondary" />
          {{ t('student.sec5Internship') }}
        </div>
        <v-table density="comfortable" class="border rounded-lg">
          <thead>
            <tr>
              <th>{{ t('student.rotationSite') }}</th>
              <th>{{ t('student.rotationType') }}</th>
              <th>{{ t('student.rotationPeriod') }}</th>
            </tr>
          </thead>
          <tbody>
            <tr v-if="!student.internships?.length">
              <td colspan="3" class="text-center cu-muted py-4">{{ t('common.none') }}</td>
            </tr>
            <tr v-for="rot in student.internships" :key="rot.externalId">
              <td class="font-weight-medium">{{ pick(rot.siteTh, rot.siteEn) }}</td>
              <td>{{ pick(rot.rotationTypeTh, rot.rotationTypeEn) }}</td>
              <td>{{ rot.periodLabel }}</td>
            </tr>
          </tbody>
        </v-table>
      </div>

      <!-- Section 8: Verified Achievements (Phase 3) -->
      <div class="mb-6">
        <div class="cu-section-title mb-2">
          <v-icon icon="mdi-trophy-outline" color="primary" />
          {{ t('student.sec8Achievements') }}
        </div>
        <v-sheet border rounded="lg" class="pa-4 text-body-2 cu-muted">
          {{ t('student.phase3PendingNote') }}
        </v-sheet>
      </div>

      <!-- Section 9: Professional License (Phase 3) -->
      <div class="mb-6">
        <div class="cu-section-title mb-2">
          <v-icon icon="mdi-certificate-outline" color="primary" />
          {{ t('student.sec9License') }}
        </div>
        <div class="text-caption cu-muted mb-2">{{ t('student.noAttachmentNote') }}</div>
        <v-sheet border rounded="lg" class="pa-4 text-body-2 cu-muted">
          {{ t('student.phase3PendingNote') }}
        </v-sheet>
      </div>

      <!-- Section 10 & 11: Graduate Employment & Employer Evaluation (Phase 3) -->
      <v-row>
        <v-col cols="12" md="6">
          <div class="cu-section-title mb-2">
            <v-icon icon="mdi-briefcase-check-outline" color="secondary" />
            {{ t('student.sec10Employment') }}
          </div>
          <v-sheet border rounded="lg" class="pa-4 h-100">
            <div class="text-caption cu-muted mb-2">{{ t('student.employmentRecheckNote') }}</div>
            <div class="text-body-2 cu-muted">{{ t('student.phase3PendingNote') }}</div>
          </v-sheet>
        </v-col>

        <v-col cols="12" md="6">
          <div class="cu-section-title mb-2">
            <v-icon icon="mdi-shield-lock-outline" color="primary" />
            {{ t('student.sec11EmployerEval') }}
          </div>
          <v-sheet border rounded="lg" class="pa-4 h-100">
            <v-alert v-if="!canViewEmployerEval" type="warning" variant="tonal" density="compact">
              {{ t('student.pdpaRestrictedEval') }}
            </v-alert>
            <div v-else class="text-body-2 cu-muted">{{ t('student.phase3PendingNote') }}</div>
          </v-sheet>
        </v-col>
      </v-row>
    </div>
  </div>

  <v-alert v-else-if="!studentsStore.loading" type="warning" variant="tonal">
    {{ t('common.noData') }}
  </v-alert>
</template>
