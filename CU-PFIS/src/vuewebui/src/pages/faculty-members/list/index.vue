<script setup>
import { computed, onMounted, ref, watch } from 'vue'
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
const snackbar = ref(false)
const snackbarMessage = ref('')
let searchTimer = null

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

async function loadList() {
  await facultyStore.fetchList({
    search: search.value.trim() || undefined,
    departmentExternalId: selectedDepartment.value || undefined,
    academicPositionExternalId: selectedPosition.value || undefined,
  })
}

onMounted(async () => {
  await Promise.all([refStore.ensureLoaded(), loadList()])
})

watch([() => authStore.personaKey, selectedDepartment, selectedPosition], loadList)

watch(search, () => {
  clearTimeout(searchTimer)
  searchTimer = setTimeout(loadList, 250)
})

function resetFilters() {
  search.value = ''
  selectedDepartment.value = null
  selectedPosition.value = null
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

    <v-alert v-if="facultyStore.error" type="error" variant="tonal" density="compact" class="mb-4">
      {{ facultyStore.error }}
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
            :disabled="authStore.isStaff || authStore.isFaculty"
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
      <v-progress-linear v-if="facultyStore.loading" indeterminate color="primary" />
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
          <tr v-if="!facultyStore.loading && facultyStore.items.length === 0">
            <td colspan="7" class="text-center py-8 cu-muted">{{ t('common.noData') }}</td>
          </tr>
          <tr v-for="item in facultyStore.items" :key="item.externalId">
            <td class="font-weight-medium">{{ item.personnelCode }}</td>
            <td>
              <div class="font-weight-bold">{{ formatFullName(item, true) }}</div>
              <div class="text-caption cu-muted">{{ item.email }}</div>
            </td>
            <td>{{ pick(item.academicPositionTitleTh, item.academicPositionTitleEn) }}</td>
            <td>{{ pick(item.departmentNameTh, item.departmentNameEn) }}</td>
            <td class="text-center">
              <v-chip size="small" color="primary" variant="tonal">
                {{ item.publicationsCount ?? 0 }}
              </v-chip>
            </td>
            <td class="text-center">
              <v-chip size="small" color="secondary" variant="tonal">
                P2
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
