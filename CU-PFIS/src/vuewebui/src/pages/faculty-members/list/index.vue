<script setup>
import { computed, ref } from 'vue'
import { useI18n } from 'vue-i18n'
import { useAbility } from '@casl/vue'
import { useAuthStore } from '@/stores/auth'
import { useFacultyMembersStore } from '@/stores/facultyMembers'
import { useReferenceDataStore } from '@/stores/referenceData'
import { useLocalizedField } from '@/composables/useLocalizedField'
import { usePdfPrint } from '@/composables/usePdfPrint'

const { t } = useI18n()
const { can } = useAbility()
const authStore = useAuthStore()
const facultyStore = useFacultyMembersStore()
const refStore = useReferenceDataStore()
const { locale, pick, formatFullName } = useLocalizedField()
const { printCurrentView } = usePdfPrint()

const search = ref('')
const selectedDepartment = ref(null)
const selectedPosition = ref(null)
const sortBy = ref('name')
const snackbar = ref(false)
const snackbarMessage = ref('')

const canCreateOrNotify = computed(() => can('edit-all', 'faculty-cv'))
const canExportAll = computed(() => can('export-all', 'faculty-cv'))

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

const filteredItems = computed(() => {
  const q = search.value.trim().toLowerCase()
  const list = facultyStore.items.filter((item) => {
    if (!authStore.canAccessFaculty(item.externalId, item.departmentExternalId)) {
      return false
    }
    if (selectedDepartment.value && item.departmentExternalId !== selectedDepartment.value) {
      return false
    }
    if (selectedPosition.value && item.academicPositionExternalId !== selectedPosition.value) {
      return false
    }
    if (!q) return true
    const haystack = [
      item.personnelCode,
      item.firstNameTh,
      item.lastNameTh,
      item.firstNameEn,
      item.lastNameEn,
      item.email,
      item.scopusAuthorId,
    ]
      .join(' ')
      .toLowerCase()
    return haystack.includes(q)
  })

  return [...list].sort((a, b) => {
    if (sortBy.value === 'publications') {
      return b.publications.length - a.publications.length
    }
    if (sortBy.value === 'updated') {
      return b.lastUpdatedAt.localeCompare(a.lastUpdatedAt)
    }
    return formatFullName(a).localeCompare(formatFullName(b))
  })
})

function departmentLabel(deptId) {
  const dept = refStore.getDepartmentById(deptId)
  return dept ? pick(dept.nameTh, dept.nameEn) : '-'
}

function positionLabel(posId) {
  const pos = refStore.getAcademicPositionById(posId)
  return pos ? pick(pos.titleTh, pos.titleEn) : '-'
}

function resetFilters() {
  search.value = ''
  selectedDepartment.value = null
  selectedPosition.value = null
  sortBy.value = 'name'
}

function handleNotifyAll() {
  snackbarMessage.value = t('faculty.notifySent')
  snackbar.value = true
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
      {{ t('common.ownCvOnlyNotice') }}
    </v-alert>

    <!-- Filters -->
    <v-card class="pa-4 mb-5 no-print">
      <v-row align="center">
        <v-col cols="12" md="4">
          <v-text-field
            v-model="search"
            :label="t('common.search')"
            prepend-inner-icon="mdi-magnify"
            clearable
            data-testid="faculty-search-input"
          />
        </v-col>
        <v-col cols="12" md="3">
          <v-select
            v-model="selectedDepartment"
            :items="departmentOptions"
            :label="t('common.department')"
          />
        </v-col>
        <v-col cols="12" md="3">
          <v-select
            v-model="selectedPosition"
            :items="positionOptions"
            :label="t('faculty.academicPosition')"
          />
        </v-col>
        <v-col cols="12" md="2" class="d-flex ga-2">
          <v-btn variant="text" block @click="resetFilters">
            {{ t('common.reset') }}
          </v-btn>
        </v-col>
      </v-row>
    </v-card>

    <!-- Data Table -->
    <v-card>
      <v-table hover data-testid="faculty-table">
        <thead>
          <tr>
            <th>{{ t('faculty.personnelCode') }}</th>
            <th>{{ t('faculty.fullName') }}</th>
            <th>{{ t('faculty.academicPosition') }}</th>
            <th>{{ t('common.department') }}</th>
            <th class="text-center">{{ t('faculty.publicationsCount') }}</th>
            <th class="text-center">{{ t('faculty.trainingsCount') }}</th>
            <th class="text-right no-print">{{ t('common.actions') }}</th>
          </tr>
        </thead>
        <tbody>
          <tr v-if="filteredItems.length === 0">
            <td colspan="7" class="text-center py-8 cu-muted">{{ t('common.noData') }}</td>
          </tr>
          <tr v-for="item in filteredItems" :key="item.externalId">
            <td class="font-weight-medium">{{ item.personnelCode }}</td>
            <td>
              <div class="font-weight-bold">{{ formatFullName(item, true) }}</div>
              <div class="text-caption cu-muted">{{ item.email }}</div>
            </td>
            <td>{{ positionLabel(item.academicPositionExternalId) }}</td>
            <td>{{ departmentLabel(item.departmentExternalId) }}</td>
            <td class="text-center">
              <v-chip size="small" color="primary" variant="tonal">
                {{ item.publications.length }}
              </v-chip>
            </td>
            <td class="text-center">
              <v-chip size="small" color="secondary" variant="tonal">
                {{ item.approvedTrainings.length }}
              </v-chip>
            </td>
            <td class="text-right no-print">
              <v-btn
                size="small"
                variant="tonal"
                color="primary"
                class="mr-2"
                :to="`/faculty-members/view/${item.externalId}`"
                :data-testid="`faculty-view-${item.personnelCode}`"
              >
                {{ t('common.view') }}
              </v-btn>
              <v-btn
                size="small"
                variant="outlined"
                :to="`/faculty-members/edit/${item.externalId}`"
              >
                {{ t('common.edit') }}
              </v-btn>
            </td>
          </tr>
        </tbody>
      </v-table>
    </v-card>

    <v-snackbar v-model="snackbar" color="success" timeout="3500">
      {{ snackbarMessage }}
    </v-snackbar>
  </div>
</template>
