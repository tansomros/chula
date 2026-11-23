import { defineStore } from 'pinia'
import { ref } from 'vue'
import { httpClient } from '@/api/httpClient'

export const useTrainingsStore = defineStore('trainings', () => {
  const items = ref([])
  const currentDetail = ref(null)
  const budgetSummary = ref(null)
  const eventSummaries = ref([])
  const policies = ref([])
  const loading = ref(false)
  const error = ref(null)

  async function fetchList(filters = {}) {
    loading.value = true
    error.value = null
    try {
      items.value = await httpClient.getTrainingRequests(filters)
      return items.value
    } catch (err) {
      error.value = err.message
      items.value = []
      return []
    } finally {
      loading.value = false
    }
  }

  async function fetchByExternalId(externalId) {
    loading.value = true
    error.value = null
    currentDetail.value = null
    try {
      currentDetail.value = await httpClient.getTrainingRequest(externalId)
      return currentDetail.value
    } catch (err) {
      error.value = err.message
      return null
    } finally {
      loading.value = false
    }
  }

  async function fetchFacultyBudgets(fiscalYearBe = null) {
    loading.value = true
    error.value = null
    try {
      budgetSummary.value = await httpClient.getFacultyBudgetSummaries(fiscalYearBe)
      return budgetSummary.value
    } catch (err) {
      error.value = err.message
      return null
    } finally {
      loading.value = false
    }
  }

  async function fetchEventSummaries(fiscalYearBe = null) {
    loading.value = true
    error.value = null
    try {
      eventSummaries.value = await httpClient.getTrainingEventSummaries(fiscalYearBe)
      return eventSummaries.value
    } catch (err) {
      error.value = err.message
      eventSummaries.value = []
      return []
    } finally {
      loading.value = false
    }
  }

  async function fetchPolicies() {
    try {
      policies.value = await httpClient.getTrainingBudgetPolicies()
      return policies.value
    } catch (err) {
      error.value = err.message
      return []
    }
  }

  async function upsertPolicy(fiscalYearBe, payload) {
    const updated = await httpClient.upsertTrainingBudgetPolicy(fiscalYearBe, payload)
    await fetchPolicies()
    return updated
  }

  async function upsertFacultyAllocation(fiscalYearBe, facultyExternalId, payload) {
    return httpClient.upsertFacultyBudgetAllocation(fiscalYearBe, facultyExternalId, payload)
  }

  async function deleteFacultyAllocation(fiscalYearBe, facultyExternalId) {
    return httpClient.deleteFacultyBudgetAllocation(fiscalYearBe, facultyExternalId)
  }

  async function createTraining(payload) {
    return httpClient.createTrainingRequest(payload)
  }

  async function updateTraining(externalId, payload) {
    await httpClient.updateTrainingRequest(externalId, { ...payload, externalId })
  }

  async function submitTraining(externalId, note = null) {
    await httpClient.submitTrainingRequest(externalId, note)
    await fetchByExternalId(externalId)
  }

  async function verifyTraining(externalId, note = null) {
    await httpClient.verifyTrainingRequest(externalId, note)
    await fetchByExternalId(externalId)
  }

  async function requestRevision(externalId, reason) {
    await httpClient.requestTrainingRevision(externalId, reason)
    await fetchByExternalId(externalId)
  }

  async function approveTraining(externalId, approvedAmount = null, note = null) {
    const res = await httpClient.approveTrainingRequest(externalId, approvedAmount, note)
    await fetchByExternalId(externalId)
    return res
  }

  async function cancelTraining(externalId, note = null) {
    await httpClient.cancelTrainingRequest(externalId, note)
    await fetchByExternalId(externalId)
  }

  async function importHistorical(itemsPayload, deductFromBudgetLedger = true) {
    return httpClient.importHistoricalTrainings(itemsPayload, deductFromBudgetLedger, 'HistoricalCsv')
  }

  return {
    items,
    currentDetail,
    budgetSummary,
    eventSummaries,
    policies,
    loading,
    error,
    fetchList,
    fetchByExternalId,
    fetchFacultyBudgets,
    fetchEventSummaries,
    fetchPolicies,
    upsertPolicy,
    upsertFacultyAllocation,
    deleteFacultyAllocation,
    createTraining,
    updateTraining,
    submitTraining,
    verifyTraining,
    requestRevision,
    approveTraining,
    cancelTraining,
    importHistorical,
  }
})
