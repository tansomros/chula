<script setup>
import { computed, onMounted, ref, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { useAuthStore } from '@/stores/auth'
import { useTrainingsStore } from '@/stores/trainings'
import { useLocalizedField } from '@/composables/useLocalizedField'
import { usePdfPrint } from '@/composables/usePdfPrint'

const { t } = useI18n()
const authStore = useAuthStore()
const trainingsStore = useTrainingsStore()
const { pick } = useLocalizedField()
const { printCurrentView } = usePdfPrint()

const selectedFiscalYear = ref(2570)
const search = ref('')
const expandedEventKey = ref(null)

const fiscalYearOptions = computed(() => {
  const fromPolicies = trainingsStore.policies.map((p) => p.fiscalYearBe)
  const years = Array.from(new Set([2568, 2569, 2570, 2571, ...fromPolicies])).sort((a, b) => b - a)
  return [
    { value: null, title: t('common.all') },
    ...years.map((fy) => ({ value: fy, title: `${t('training.fiscalYear')} ${fy}` })),
  ]
})

const filteredEvents = computed(() => {
  const q = search.value.trim().toLowerCase()
  if (!q) return trainingsStore.eventSummaries
  return trainingsStore.eventSummaries.filter((ev) => {
    const haystack = [ev.titleTh, ev.titleEn, ev.organizerAndVenueTh, ev.organizerAndVenueEn]
      .filter(Boolean)
      .join(' ')
      .toLowerCase()
    return haystack.includes(q)
  })
})

function formatMoney(val) {
  return Number(val || 0).toLocaleString('en-US', {
    minimumFractionDigits: 2,
    maximumFractionDigits: 2,
  })
}

function toggleEventExpand(key) {
  expandedEventKey.value = expandedEventKey.value === key ? null : key
}

async function loadEvents() {
  await Promise.all([
    trainingsStore.fetchPolicies(),
    trainingsStore.fetchEventSummaries(selectedFiscalYear.value || null),
  ])
}

onMounted(loadEvents)
watch([() => authStore.personaKey, selectedFiscalYear], loadEvents)
</script>

<template>
  <div>
    <div class="cu-page-header">
      <div>
        <h1 class="text-h5 font-weight-bold">{{ t('training.tabEvents') }}</h1>
        <p class="text-body-2 cu-muted mb-0">{{ t('training.subtitle') }}</p>
      </div>

      <div class="d-flex flex-wrap ga-2 no-print">
        <v-btn variant="outlined" prepend-icon="mdi-printer-outline" @click="printCurrentView">
          {{ t('common.exportPdf') }}
        </v-btn>
        <v-btn color="primary" prepend-icon="mdi-plus" to="/trainings/create">
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

    <!-- Filters -->
    <v-card class="pa-4 mb-5 no-print">
      <v-row align="center">
        <v-col cols="12" md="4">
          <v-select
            v-model="selectedFiscalYear"
            :items="fiscalYearOptions"
            :label="t('training.fiscalYear')"
            hide-details
            data-testid="events-fy-select"
          />
        </v-col>
        <v-col cols="12" md="8">
          <v-text-field
            v-model="search"
            :label="t('common.search')"
            prepend-inner-icon="mdi-magnify"
            clearable
            hide-details
            data-testid="events-search-input"
          />
        </v-col>
      </v-row>
    </v-card>

    <!-- Per-Event Summary Table -->
    <v-card>
      <v-progress-linear v-if="trainingsStore.loading" indeterminate color="primary" />
      <v-table density="comfortable" hover data-testid="event-summaries-table">
        <thead>
          <tr>
            <th>{{ t('faculty.trainingTitle') }}</th>
            <th>{{ t('training.workTypeLabel') }}</th>
            <th>{{ t('faculty.trainingDate') }}</th>
            <th class="text-center">{{ t('training.attendeesCount') }}</th>
            <th class="text-right">{{ t('training.avgCostPerPerson') }}</th>
            <th class="text-right">{{ t('training.totalCost') }}</th>
            <th class="text-center no-print">{{ t('common.actions') }}</th>
          </tr>
        </thead>
        <tbody>
          <tr v-if="!trainingsStore.loading && filteredEvents.length === 0">
            <td colspan="7" class="text-center py-8 cu-muted">{{ t('common.noData') }}</td>
          </tr>
          <template v-for="(ev, idx) in filteredEvents" :key="`${ev.titleTh}-${ev.startDate}-${idx}`">
            <tr>
              <td>
                <div class="font-weight-bold">{{ pick(ev.titleTh, ev.titleEn) }}</div>
                <div class="text-caption cu-muted">{{ pick(ev.organizerAndVenueTh, ev.organizerAndVenueEn) }}</div>
              </td>
              <td>{{ t(`training.workType.${ev.workType}`) }}</td>
              <td class="text-no-wrap">{{ ev.startDate }} – {{ ev.endDate }}</td>
              <td class="text-center">
                <v-chip size="small" color="primary" variant="tonal">{{ ev.attendeeCount }}</v-chip>
              </td>
              <td class="text-right">{{ formatMoney(ev.averageAmountPerAttendee) }}</td>
              <td class="text-right font-weight-bold">{{ formatMoney(ev.totalApprovedAmount) }}</td>
              <td class="text-center no-print">
                <v-btn
                  size="x-small"
                  variant="tonal"
                  color="secondary"
                  :icon="expandedEventKey === `${ev.titleTh}-${ev.startDate}-${idx}` ? 'mdi-chevron-up' : 'mdi-account-group-outline'"
                  @click="toggleEventExpand(`${ev.titleTh}-${ev.startDate}-${idx}`)"
                />
              </td>
            </tr>
            <tr v-if="expandedEventKey === `${ev.titleTh}-${ev.startDate}-${idx}`" class="bg-surface-variant">
              <td colspan="7" class="pa-4">
                <v-list density="compact" class="rounded border bg-surface">
                  <v-list-item
                    v-for="att in ev.attendees"
                    :key="att.trainingRequestExternalId"
                    :to="`/trainings/view/${att.trainingRequestExternalId}`"
                  >
                    <template #prepend>
                      <v-icon icon="mdi-account-tie-outline" color="primary" size="18" />
                    </template>
                    <v-list-item-title>
                      {{ pick(att.facultyNameTh, att.facultyNameEn) }}
                      <span class="text-caption cu-muted">({{ pick(att.departmentNameTh, att.departmentNameEn) }})</span>
                    </v-list-item-title>
                    <template #append>
                      <v-chip size="x-small" color="primary" variant="tonal">
                        {{ formatMoney(att.approvedAmount) }} THB
                      </v-chip>
                    </template>
                  </v-list-item>
                </v-list>
              </td>
            </tr>
          </template>
        </tbody>
      </v-table>
    </v-card>
  </div>
</template>
