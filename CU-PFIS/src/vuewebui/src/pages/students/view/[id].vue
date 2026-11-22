<script setup>
import { computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { useRoute } from 'vue-router'
import { useAbility } from '@casl/vue'
import { useStudentsStore } from '@/stores/students'
import { useReferenceDataStore } from '@/stores/referenceData'
import { useLocalizedField } from '@/composables/useLocalizedField'
import { usePdfPrint } from '@/composables/usePdfPrint'

const { t } = useI18n()
const route = useRoute()
const { can } = useAbility()
const studentsStore = useStudentsStore()
const refStore = useReferenceDataStore()
const { locale, pick, formatFullName } = useLocalizedField()
const { printCurrentView } = usePdfPrint()

const student = computed(() => studentsStore.getByExternalId(route.params.id))

const major = computed(() =>
  student.value ? refStore.getMajorById(student.value.majorExternalId) : null,
)

// Rule: Only Staff-Verified achievements appear on Student CV
const verifiedAchievements = computed(() =>
  (student.value?.achievements || []).filter((a) => a.status === 'Verified'),
)
const pendingAchievementsCount = computed(() =>
  (student.value?.achievements || []).filter((a) => a.status !== 'Verified').length,
)

const canViewEmployerEval = computed(() => can('view', 'employer-evaluations'))

const licenseColumns = [
  { key: 'cc1', label: 'CC1' },
  { key: 'cc2', label: 'CC2' },
  { key: 'ip1', label: 'IP1' },
  { key: 'ip2', label: 'IP2' },
  { key: 'pc1', label: 'PC1' },
  { key: 'pc2', label: 'PC2' },
]
</script>

<template>
  <div v-if="student">
    <div class="cu-page-header no-print">
      <div class="d-flex align-center ga-3">
        <v-btn icon="mdi-arrow-left" variant="outlined" to="/students/list" />
        <div>
          <h1 class="text-h5 font-weight-bold">{{ t('student.viewTitle') }}</h1>
          <div class="text-caption cu-muted">
            {{ student.studentCode }} · {{ t('student.lastSynced') }}: {{ student.importedAt }}
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
            {{ student.sourceSystem }} ({{ student.sourceRecordId }})
          </v-chip>
          <v-chip v-if="student.staffVerifiedEdit" color="success" variant="tonal" size="small">
            <v-icon start icon="mdi-shield-check-outline" size="14" />
            {{ t('common.verified') }}
          </v-chip>
        </div>
      </div>

      <!-- Sections 1, 2, 3, 4, 6 -->
      <v-row class="mb-6">
        <v-col cols="12" md="6">
          <div class="cu-section-title mb-2">
            <v-icon icon="mdi-card-account-details-outline" color="secondary" />
            {{ t('student.sec1General') }}
          </div>
          <v-sheet border rounded="lg" class="pa-4 mb-4">
            <div class="mb-2">
              <span class="text-caption cu-muted">{{ t('student.cohortYear') }}:</span>
              <strong class="ml-2">{{ student.cohortAcademicYearBe }}</strong>
              <span class="mx-2">·</span>
              <span class="text-caption cu-muted">{{ t('student.studyYear') }}:</span>
              <strong class="ml-2">{{ student.studyYear }}</strong>
            </div>
            <div class="mb-2">
              <span class="text-caption cu-muted">{{ t('common.major') }}:</span>
              <strong class="ml-2">{{ major ? pick(major.nameTh, major.nameEn) : '-' }}</strong>
            </div>
            <div>
              <span class="text-caption cu-muted">{{ t('student.sec6Admission') }}:</span>
              <v-chip size="small" color="primary" variant="tonal" class="ml-2">
                {{ refStore.getAdmissionRoundLabel(student.admissionRoundExternalId, locale) }}
              </v-chip>
            </div>
          </v-sheet>

          <div class="cu-section-title mb-2">
            <v-icon icon="mdi-town-hall" color="secondary" />
            {{ t('student.sec4HighSchool') }}
          </div>
          <v-sheet border rounded="lg" class="pa-4">
            {{ pick(student.highSchoolTh, student.highSchoolEn) }}
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
              {{ pick(student.academicAdvisorTh, student.academicAdvisorEn) }}
            </div>
            <div class="text-caption cu-muted">{{ t('student.sec3ProjectAdvisor') }}</div>
            <div class="font-weight-medium">
              {{ pick(student.projectAdvisorTh, student.projectAdvisorEn) }}
            </div>
          </v-sheet>

          <!-- Section 7: Senior Project -->
          <div class="cu-section-title mb-2">
            <v-icon icon="mdi-book-open-page-variant-outline" color="secondary" />
            {{ t('student.sec7Project') }}
          </div>
          <v-sheet border rounded="lg" class="pa-4">
            <div class="font-weight-medium mb-1">{{ student.seniorProject.titleTh }}</div>
            <div class="text-body-2 cu-muted mb-2">{{ student.seniorProject.titleEn }}</div>
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
            <tr v-for="(rot, idx) in student.internships" :key="idx">
              <td class="font-weight-medium">{{ pick(rot.siteTh, rot.siteEn) }}</td>
              <td>{{ pick(rot.typeTh, rot.typeEn) }}</td>
              <td>{{ rot.period }}</td>
            </tr>
          </tbody>
        </v-table>
      </div>

      <!-- Section 8: Verified Achievements -->
      <div class="mb-6">
        <div class="cu-section-title mb-2">
          <v-icon icon="mdi-trophy-outline" color="primary" />
          {{ t('student.sec8Achievements') }}
        </div>
        <v-alert
          v-if="pendingAchievementsCount > 0"
          type="warning"
          variant="tonal"
          density="compact"
          class="mb-3 no-print"
        >
          {{ t('student.hiddenPendingCount', { count: pendingAchievementsCount }) }}
        </v-alert>
        <v-table density="comfortable" class="border rounded-lg">
          <thead>
            <tr>
              <th>{{ t('faculty.trainingTitle') }}</th>
              <th>Award</th>
              <th>Date</th>
              <th class="text-center">Status</th>
            </tr>
          </thead>
          <tbody>
            <tr v-if="verifiedAchievements.length === 0">
              <td colspan="4" class="text-center cu-muted py-4">{{ t('common.none') }}</td>
            </tr>
            <tr v-for="(ach, idx) in verifiedAchievements" :key="idx">
              <td class="font-weight-medium">{{ pick(ach.eventNameTh, ach.eventNameEn) }}</td>
              <td>{{ pick(ach.awardTh, ach.awardEn) }}</td>
              <td>{{ ach.eventDate }}</td>
              <td class="text-center">
                <v-chip size="x-small" color="success" variant="tonal">
                  {{ t('common.verified') }}
                </v-chip>
              </td>
            </tr>
          </tbody>
        </v-table>
      </div>

      <!-- Section 9: Professional License -->
      <div class="mb-6">
        <div class="cu-section-title mb-2">
          <v-icon icon="mdi-certificate-outline" color="primary" />
          {{ t('student.sec9License') }}
        </div>
        <div class="text-caption cu-muted mb-3">{{ t('student.noAttachmentNote') }}</div>
        <v-sheet border rounded="lg" class="pa-4 d-flex flex-wrap align-center ga-3">
          <v-chip
            v-for="col in licenseColumns"
            :key="col.key"
            :color="student.license[col.key] ? 'success' : 'default'"
            :variant="student.license[col.key] ? 'flat' : 'outlined'"
          >
            <v-icon
              start
              :icon="student.license[col.key] ? 'mdi-check-circle' : 'mdi-minus-circle-outline'"
              size="16"
            />
            {{ col.label }}
          </v-chip>
        </v-sheet>
      </div>

      <!-- Section 10 & 11: Graduate Employment & Employer Evaluation -->
      <v-row>
        <v-col cols="12" md="6">
          <div class="cu-section-title mb-2">
            <v-icon icon="mdi-briefcase-check-outline" color="secondary" />
            {{ t('student.sec10Employment') }}
          </div>
          <v-sheet border rounded="lg" class="pa-4 h-100">
            <div class="d-flex justify-space-between align-center mb-3">
              <span class="text-caption cu-muted">{{ t('student.employmentRecheckNote') }}</span>
              <v-chip
                size="x-small"
                :color="student.employment.verificationStatus === 'RecheckedVerified' ? 'success' : 'warning'"
                variant="tonal"
              >
                {{
                  student.employment.verificationStatus === 'RecheckedVerified'
                    ? t('common.rechecked')
                    : t('common.pendingReview')
                }}
              </v-chip>
            </div>
            <div class="mb-2">
              <span class="text-caption cu-muted">{{ t('student.jobTitle') }}:</span>
              <strong class="ml-2">
                {{ pick(student.employment.jobTitleTh, student.employment.jobTitleEn) }}
              </strong>
            </div>
            <div>
              <span class="text-caption cu-muted">{{ t('student.workplace') }}:</span>
              <strong class="ml-2">
                {{ pick(student.employment.workplaceTh, student.employment.workplaceEn) }}
              </strong>
            </div>
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
            <div v-else-if="student.employerEvaluation">
              <div class="mb-2">
                <span class="text-caption cu-muted">{{ t('student.supervisor') }}:</span>
                <strong class="ml-2">
                  {{ pick(student.employerEvaluation.supervisorNameTh, student.employerEvaluation.supervisorNameEn) }}
                </strong>
              </div>
              <div class="mb-2">
                <span class="text-caption cu-muted">{{ t('student.satisfactionScore') }}:</span>
                <v-chip size="small" color="success" variant="tonal" class="ml-2">
                  {{ student.employerEvaluation.satisfactionScore }}
                </v-chip>
              </div>
              <div class="text-body-2 mt-2">
                "{{ pick(student.employerEvaluation.commentTh, student.employerEvaluation.commentEn) }}"
              </div>
            </div>
            <div v-else class="text-body-2 cu-muted">{{ t('common.none') }}</div>
          </v-sheet>
        </v-col>
      </v-row>
    </div>
  </div>

  <v-alert v-else type="warning" variant="tonal">
    {{ t('common.noData') }}
  </v-alert>
</template>
