<script setup>
import { computed, onMounted, ref, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { useRoute } from 'vue-router'
import { useAuthStore } from '@/stores/auth'
import { useFacultyMembersStore } from '@/stores/facultyMembers'
import { useLocalizedField } from '@/composables/useLocalizedField'
import { usePdfPrint } from '@/composables/usePdfPrint'

const { t } = useI18n()
const route = useRoute()
const authStore = useAuthStore()
const facultyStore = useFacultyMembersStore()
const { pick, formatFullName } = useLocalizedField()
const { printCurrentView } = usePdfPrint()

const fileInputRef = ref(null)
const snackbar = ref(false)
const snackbarText = ref('')
const snackbarColor = ref('success')

const member = computed(() => facultyStore.currentDetail)

const formattedLastUpdated = computed(() => {
  if (!member.value?.lastUpdatedAt) return '-'
  return new Date(member.value.lastUpdatedAt).toLocaleString()
})

async function loadMember() {
  if (!route.params.id) return
  await facultyStore.fetchByExternalId(route.params.id)
}

onMounted(loadMember)
watch([() => route.params.id, () => authStore.personaKey], loadMember)

function triggerCsvUpload() {
  fileInputRef.value?.click()
}

// A16 Phase 1 CSV Import (parses Scopus CSV columns: Title, Year, Source title, DOI, EID)
function handleScopusCsvFile(event) {
  const file = event.target.files?.[0]
  if (!file || !member.value) return

  const reader = new FileReader()
  reader.onload = async () => {
    const text = String(reader.result || '')
    const lines = text.split(/\r?\n/).filter((l) => l.trim())
    const parsed = []

    for (let i = 1; i < lines.length; i += 1) {
      const cols = lines[i].split(',').map((c) => c.replace(/^"|"$/g, '').trim())
      if (!cols[0]) continue
      parsed.push({
        title: cols[0],
        publishedYear: Number(cols[1]) || new Date().getFullYear(),
        journalOrVenue: cols[2] || 'Scopus Imported',
        doi: cols[3] || null,
        scopusId: cols[4] || null,
      })
    }

    try {
      const result = await facultyStore.importScopusPublications(member.value.externalId, parsed)
      snackbarColor.value = 'success'
      snackbarText.value = t('faculty.scopusImported', {
        added: result.addedCount,
        skipped: result.skippedDuplicateCount,
      })
      snackbar.value = true
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
  <v-progress-linear v-if="facultyStore.loading" indeterminate color="primary" class="mb-4" />

  <v-alert v-if="facultyStore.error" type="error" variant="tonal" class="mb-4">
    {{ facultyStore.error }}
  </v-alert>

  <div v-if="member">
    <div class="cu-page-header no-print">
      <div class="d-flex align-center ga-3">
        <v-btn icon="mdi-arrow-left" variant="outlined" to="/faculty-members/list" />
        <div>
          <h1 class="text-h5 font-weight-bold">{{ t('faculty.viewTitle') }}</h1>
          <div class="text-caption cu-muted">
            {{ member.personnelCode }} · {{ t('faculty.lastUpdated') }}: {{ formattedLastUpdated }}
          </div>
        </div>
      </div>

      <div class="d-flex flex-wrap ga-2">
        <input
          ref="fileInputRef"
          type="file"
          accept=".csv"
          class="d-none"
          @change="handleScopusCsvFile"
        />
        <v-btn
          variant="outlined"
          color="secondary"
          prepend-icon="mdi-database-import-outline"
          :loading="facultyStore.loading"
          @click="triggerCsvUpload"
        >
          {{ t('faculty.importScopus') }}
        </v-btn>
        <v-btn
          variant="outlined"
          prepend-icon="mdi-printer-outline"
          data-testid="faculty-export-pdf-btn"
          @click="printCurrentView"
        >
          {{ t('common.printProfile') }}
        </v-btn>
        <v-btn
          color="primary"
          prepend-icon="mdi-pencil-outline"
          :to="`/faculty-members/edit/${member.externalId}`"
        >
          {{ t('common.edit') }}
        </v-btn>
      </div>
    </div>

    <!-- Print-ready 8-section Faculty Profile Sheet -->
    <div class="cu-cv-sheet pa-6 pa-md-8">
      <!-- Header Banner -->
      <div class="d-flex flex-wrap justify-space-between align-start pb-5 mb-6 border-b">
        <div>
          <div class="text-caption font-weight-bold text-primary mb-1">
            {{ t('app.faculty') }} ({{ t('app.acronym') }})
          </div>
          <h2 class="text-h5 font-weight-bold">{{ formatFullName(member, true) }}</h2>
          <div class="text-body-2 cu-muted mt-1">
            {{ member.firstNameTh }} {{ member.lastNameTh }} / {{ member.firstNameEn }} {{ member.lastNameEn }}
          </div>
        </div>
        <div class="text-md-right mt-3 mt-md-0">
          <v-chip color="primary" variant="tonal" class="mb-2">
            {{ pick(member.academicPositionTitleTh, member.academicPositionTitleEn) }}
          </v-chip>
          <div class="text-body-2">
            {{ pick(member.departmentNameTh, member.departmentNameEn) }}
          </div>
        </div>
      </div>

      <!-- Sections 1–5 -->
      <v-row class="mb-6">
        <v-col cols="12" md="6">
          <div class="text-caption cu-muted">{{ t('faculty.sec3Admin') }}</div>
          <div class="text-body-1 font-weight-medium mb-3">
            {{ pick(member.administrativePositionsTh, member.administrativePositionsEn) || '-' }}
          </div>
          <div class="text-caption cu-muted">{{ t('faculty.sec4Email') }}</div>
          <div class="text-body-1 mb-3">{{ member.email }}</div>
        </v-col>
        <v-col cols="12" md="6">
          <div class="text-caption cu-muted">{{ t('faculty.sec5Phone') }}</div>
          <div class="text-body-1 mb-3">{{ member.phoneNumber || '-' }}</div>
          <div class="text-caption cu-muted">Scopus Author ID / ORCID iD</div>
          <div class="d-flex flex-wrap ga-2 mt-1">
            <v-chip size="small" variant="outlined" prepend-icon="mdi-book-search-outline">
              Scopus: {{ member.scopusAuthorId || '-' }}
            </v-chip>
            <v-chip size="small" variant="outlined" prepend-icon="mdi-identifier">
              ORCID: {{ member.orcidId || '-' }}
            </v-chip>
          </div>
        </v-col>
      </v-row>

      <!-- Section 6: Education -->
      <div class="mb-6">
        <div class="cu-section-title mb-3">
          <v-icon icon="mdi-school-outline" color="primary" />
          {{ t('faculty.sec6Education') }}
        </div>
        <v-table density="comfortable" class="border rounded-lg">
          <thead>
            <tr>
              <th>{{ t('faculty.degree') }}</th>
              <th>{{ t('faculty.institution') }}</th>
              <th class="text-center">{{ t('faculty.gradYear') }}</th>
            </tr>
          </thead>
          <tbody>
            <tr v-if="!member.educations?.length">
              <td colspan="3" class="text-center cu-muted py-4">{{ t('common.none') }}</td>
            </tr>
            <tr v-for="edu in member.educations" :key="edu.externalId">
              <td class="font-weight-medium">{{ pick(edu.degreeTh, edu.degreeEn) }}</td>
              <td>{{ pick(edu.institutionTh, edu.institutionEn) }}</td>
              <td class="text-center">{{ edu.graduationYear }}</td>
            </tr>
          </tbody>
        </v-table>
      </div>

      <!-- Section 7: Publications -->
      <div class="mb-6">
        <div class="d-flex flex-wrap justify-space-between align-center mb-3">
          <div class="cu-section-title">
            <v-icon icon="mdi-file-document-multiple-outline" color="primary" />
            {{ t('faculty.sec7Publications') }}
          </div>
          <span class="text-caption cu-muted no-print">{{ t('faculty.scopusImportHint') }}</span>
        </div>

        <v-table density="comfortable" class="border rounded-lg">
          <thead>
            <tr>
              <th>{{ t('faculty.pubTitle') }}</th>
              <th>{{ t('faculty.journal') }}</th>
              <th class="text-center">{{ t('faculty.pubYear') }}</th>
              <th>{{ t('faculty.scopusId') }} / {{ t('faculty.doi') }}</th>
            </tr>
          </thead>
          <tbody>
            <tr v-if="!member.publications?.length">
              <td colspan="4" class="text-center cu-muted py-4">{{ t('common.none') }}</td>
            </tr>
            <tr v-for="pub in member.publications" :key="pub.externalId">
              <td class="font-weight-medium">{{ pub.title }}</td>
              <td>{{ pub.journalOrVenue || '-' }}</td>
              <td class="text-center">{{ pub.publishedYear }}</td>
              <td>
                <div v-if="pub.scopusId" class="text-caption">EID: {{ pub.scopusId }}</div>
                <div v-if="pub.doi" class="text-caption cu-muted">DOI: {{ pub.doi }}</div>
              </td>
            </tr>
          </tbody>
        </v-table>
      </div>

      <!-- Section 8: Auto-linked Training History (Phase 2) -->
      <div>
        <div class="cu-section-title mb-2">
          <v-icon icon="mdi-calendar-check-outline" color="secondary" />
          {{ t('faculty.sec8Training') }}
        </div>
        <v-alert type="info" variant="tonal" density="compact" class="mb-3 no-print">
          {{ t('faculty.autoLinkedTrainingNote') }}
        </v-alert>

        <v-table density="comfortable" class="border rounded-lg">
          <thead>
            <tr>
              <th>{{ t('faculty.trainingTitle') }}</th>
              <th>{{ t('faculty.trainingWorkType') }}</th>
              <th>{{ t('faculty.trainingCategory') }}</th>
              <th>{{ t('faculty.trainingDate') }}</th>
            </tr>
          </thead>
          <tbody>
            <tr v-if="!member.approvedTrainings?.length">
              <td colspan="4" class="text-center cu-muted py-4">{{ t('common.none') }}</td>
            </tr>
            <tr v-for="tr in member.approvedTrainings" :key="tr.externalId">
              <td>
                <router-link :to="`/trainings/view/${tr.externalId}`" class="font-weight-medium text-primary text-decoration-none">
                  {{ pick(tr.titleTh, tr.titleEn) }}
                </router-link>
                <div class="text-caption cu-muted">{{ pick(tr.organizerAndVenueTh, tr.organizerAndVenueEn) }}</div>
              </td>
              <td>{{ t(`training.workType.${tr.workType}`) }}</td>
              <td>
                <v-chip size="x-small" color="secondary" variant="tonal">
                  {{ t(`training.category.${tr.category}`) }}
                </v-chip>
              </td>
              <td class="text-no-wrap">{{ tr.startDate }} – {{ tr.endDate }}</td>
            </tr>
          </tbody>
        </v-table>
      </div>
    </div>

    <v-snackbar v-model="snackbar" :color="snackbarColor" timeout="4000">
      {{ snackbarText }}
    </v-snackbar>
  </div>

  <v-alert v-else-if="!facultyStore.loading" type="warning" variant="tonal">
    {{ t('common.noData') }}
  </v-alert>
</template>
