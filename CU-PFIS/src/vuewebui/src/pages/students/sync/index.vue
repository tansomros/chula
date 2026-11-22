<script setup>
import { onMounted, ref } from 'vue'
import { useI18n } from 'vue-i18n'
import { httpClient } from '@/api/httpClient'

const { t } = useI18n()

const loading = ref(false)
const syncing = ref(false)
const resolvingId = ref(null)
const health = ref(null)
const batches = ref([])
const conflicts = ref([])
const maxRecords = ref(50)
const errorMessage = ref('')
const snackbar = ref(false)
const snackbarText = ref('')

async function loadSyncDashboard() {
  loading.value = true
  errorMessage.value = ''
  try {
    const [healthRes, batchesRes, conflictsRes] = await Promise.all([
      httpClient.getCuppepHealth(),
      httpClient.getImportBatches(15),
      httpClient.getDataConflicts('Pending'),
    ])
    health.value = healthRes
    batches.value = batchesRes
    conflicts.value = conflictsRes
  } catch (err) {
    errorMessage.value = err.message || t('common.apiError')
  } finally {
    loading.value = false
  }
}

async function handleRunSync() {
  syncing.value = true
  errorMessage.value = ''
  try {
    const payload = {}
    if (maxRecords.value && Number(maxRecords.value) > 0) {
      payload.maxRecords = Number(maxRecords.value)
    }
    const batch = await httpClient.syncCuppepStudents(payload)
    snackbarText.value = t('student.syncCompleted', {
      pulled: batch.totalPulledCount,
      created: batch.createdCount,
      updated: batch.updatedCount,
      skipped: batch.skippedUnchangedCount,
      conflicts: batch.conflictCount,
    })
    snackbar.value = true
    await loadSyncDashboard()
  } catch (err) {
    errorMessage.value = err.message || t('common.apiError')
    await loadSyncDashboard()
  } finally {
    syncing.value = false
  }
}

async function handleResolve(conflictExternalId, action) {
  resolvingId.value = conflictExternalId
  errorMessage.value = ''
  try {
    await httpClient.resolveDataConflict(conflictExternalId, action)
    await loadSyncDashboard()
  } catch (err) {
    errorMessage.value = err.message || t('common.apiError')
  } finally {
    resolvingId.value = null
  }
}

function parseSnapshot(json) {
  try {
    return JSON.parse(json)
  } catch {
    return {}
  }
}

function statusColor(status) {
  if (status === 'Completed') return 'success'
  if (status === 'CompletedWithConflicts') return 'warning'
  if (status === 'Failed') return 'error'
  return 'info'
}

onMounted(loadSyncDashboard)
</script>

<template>
  <div>
    <div class="cu-page-header">
      <div class="d-flex align-center ga-3">
        <v-btn icon="mdi-arrow-left" variant="outlined" to="/students/list" />
        <div>
          <h1 class="text-h5 font-weight-bold">{{ t('student.syncPageTitle') }}</h1>
          <p class="text-body-2 cu-muted mb-0">{{ t('student.syncPageSubtitle') }}</p>
        </div>
      </div>
    </div>

    <v-progress-linear v-if="loading || syncing" indeterminate color="secondary" class="mb-4" />

    <v-alert v-if="errorMessage" type="error" variant="tonal" class="mb-4">
      {{ errorMessage }}
    </v-alert>

    <!-- Health & Trigger Card -->
    <v-card class="pa-5 mb-6">
      <v-row align="center">
        <v-col cols="12" md="5">
          <div class="text-caption cu-muted mb-1">{{ t('student.cuppepApiStatus') }}</div>
          <div class="d-flex align-center ga-2">
            <v-chip
              :color="health?.isReachable ? 'success' : 'error'"
              variant="tonal"
              size="small"
              data-testid="cuppep-health-chip"
            >
              <v-icon
                start
                :icon="health?.isReachable ? 'mdi-check-circle' : 'mdi-lan-disconnect'"
                size="14"
              />
              {{ health?.isReachable ? t('student.cuppepOnline') : t('student.cuppepOffline') }}
            </v-chip>
            <span class="text-caption cu-muted">{{ health?.baseUrl }}</span>
          </div>
        </v-col>

        <v-col cols="12" md="4">
          <v-text-field
            v-model.number="maxRecords"
            :label="t('student.syncMaxRecords')"
            type="number"
            min="1"
            max="5000"
            clearable
            hide-details
            density="compact"
            data-testid="cuppep-max-records-input"
          />
        </v-col>

        <v-col cols="12" md="3" class="text-md-right">
          <v-btn
            color="secondary"
            :loading="syncing"
            prepend-icon="mdi-cloud-sync-outline"
            block
            data-testid="cuppep-run-sync-btn"
            @click="handleRunSync"
          >
            {{ t('student.runSyncNow') }}
          </v-btn>
        </v-col>
      </v-row>
    </v-card>

    <!-- Pending Data Conflict Queue -->
    <v-card class="pa-5 mb-6">
      <div class="cu-section-title mb-2">
        <v-icon icon="mdi-alert-decagram-outline" color="warning" />
        {{ t('student.conflictQueueTitle') }} ({{ conflicts.length }})
      </div>
      <p class="text-body-2 cu-muted mb-4">{{ t('student.conflictQueueHint') }}</p>

      <v-alert v-if="conflicts.length === 0" type="success" variant="tonal" density="compact">
        {{ t('student.noConflicts') }}
      </v-alert>

      <v-card
        v-for="item in conflicts"
        :key="item.externalId"
        variant="outlined"
        class="pa-4 mb-3"
        :data-testid="`conflict-item-${item.businessKey}`"
      >
        <div class="d-flex flex-wrap justify-space-between align-center ga-2 mb-3">
          <div>
            <span class="font-weight-bold mr-2">{{ item.businessKey }}</span>
            <v-chip size="x-small" color="warning" variant="tonal">
              {{ t('student.changedFields') }}: {{ item.changedFieldsSummary }}
            </v-chip>
          </div>
          <div class="d-flex ga-2">
            <v-btn
              size="small"
              variant="outlined"
              color="primary"
              :loading="resolvingId === item.externalId"
              @click="handleResolve(item.externalId, 'KeepCuPfis')"
            >
              {{ t('student.keepCuPfis') }}
            </v-btn>
            <v-btn
              size="small"
              color="secondary"
              :loading="resolvingId === item.externalId"
              @click="handleResolve(item.externalId, 'AcceptIncoming')"
            >
              {{ t('student.acceptCuppep') }}
            </v-btn>
          </div>
        </div>

        <v-row dense>
          <v-col cols="12" md="6">
            <v-sheet border rounded class="pa-3 bg-surface-variant">
              <div class="text-caption font-weight-bold mb-1">{{ t('student.currentValues') }}</div>
              <div class="text-body-2">
                {{ parseSnapshot(item.currentValuesJson).firstNameTh }}
                {{ parseSnapshot(item.currentValuesJson).lastNameTh }}
                ({{ parseSnapshot(item.currentValuesJson).majorCode }}, Year {{ parseSnapshot(item.currentValuesJson).studyYear }})
              </div>
              <div class="text-caption cu-muted">
                {{ parseSnapshot(item.currentValuesJson).highSchoolTh || '-' }}
              </div>
            </v-sheet>
          </v-col>
          <v-col cols="12" md="6">
            <v-sheet border rounded class="pa-3">
              <div class="text-caption font-weight-bold text-secondary mb-1">
                {{ t('student.incomingValues') }}
              </div>
              <div class="text-body-2">
                {{ parseSnapshot(item.incomingValuesJson).firstNameTh }}
                {{ parseSnapshot(item.incomingValuesJson).lastNameTh }}
                ({{ parseSnapshot(item.incomingValuesJson).majorCode }}, Year {{ parseSnapshot(item.incomingValuesJson).studyYear }})
              </div>
              <div class="text-caption cu-muted">
                {{ parseSnapshot(item.incomingValuesJson).highSchoolTh || '-' }}
              </div>
            </v-sheet>
          </v-col>
        </v-row>
      </v-card>
    </v-card>

    <!-- Recent Import Batches -->
    <v-card class="pa-5">
      <div class="cu-section-title mb-3">
        <v-icon icon="mdi-history" color="primary" />
        {{ t('student.importHistory') }}
      </div>
      <v-table density="comfortable" data-testid="import-batches-table">
        <thead>
          <tr>
            <th>Started</th>
            <th>{{ t('student.batchStatus') }}</th>
            <th class="text-center">{{ t('student.batchPulled') }}</th>
            <th class="text-center">{{ t('student.batchCreated') }}</th>
            <th class="text-center">{{ t('student.batchUpdated') }}</th>
            <th class="text-center">{{ t('student.batchSkipped') }}</th>
            <th class="text-center">{{ t('student.batchConflicts') }}</th>
          </tr>
        </thead>
        <tbody>
          <tr v-if="batches.length === 0">
            <td colspan="7" class="text-center py-6 cu-muted">{{ t('common.noData') }}</td>
          </tr>
          <tr v-for="b in batches" :key="b.externalId">
            <td>{{ new Date(b.startedAt).toLocaleString() }}</td>
            <td>
              <v-chip size="x-small" :color="statusColor(b.status)" variant="tonal">
                {{ b.status }}
              </v-chip>
            </td>
            <td class="text-center">{{ b.totalPulledCount }}</td>
            <td class="text-center">{{ b.createdCount }}</td>
            <td class="text-center">{{ b.updatedCount }}</td>
            <td class="text-center">{{ b.skippedUnchangedCount }}</td>
            <td class="text-center">{{ b.conflictCount }}</td>
          </tr>
        </tbody>
      </v-table>
    </v-card>

    <v-snackbar v-model="snackbar" color="success" timeout="5000">
      {{ snackbarText }}
    </v-snackbar>
  </div>
</template>
