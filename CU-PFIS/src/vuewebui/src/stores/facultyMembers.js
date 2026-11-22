import { defineStore } from 'pinia'
import { ref } from 'vue'
import { httpClient } from '@/api/httpClient'

export const useFacultyMembersStore = defineStore('facultyMembers', () => {
  const items = ref([])
  const currentDetail = ref(null)
  const loading = ref(false)
  const error = ref(null)

  async function fetchList(filters = {}) {
    loading.value = true
    error.value = null
    try {
      items.value = await httpClient.getFacultyMembers(filters)
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
      currentDetail.value = await httpClient.getFacultyMember(externalId)
      return currentDetail.value
    } catch (err) {
      error.value = err.message
      return null
    } finally {
      loading.value = false
    }
  }

  async function createMember(payload) {
    loading.value = true
    error.value = null
    try {
      const externalId = await httpClient.createFacultyMember(payload)
      return externalId
    } catch (err) {
      error.value = err.message
      throw err
    } finally {
      loading.value = false
    }
  }

  async function updateMember(externalId, payload) {
    loading.value = true
    error.value = null
    try {
      await httpClient.updateFacultyMember(externalId, { ...payload, externalId })
    } catch (err) {
      error.value = err.message
      throw err
    } finally {
      loading.value = false
    }
  }

  async function importScopusPublications(externalId, incomingPublications) {
    loading.value = true
    error.value = null
    try {
      const result = await httpClient.importScopusPublications(externalId, incomingPublications, 'ScopusCsv')
      await fetchByExternalId(externalId)
      return result
    } catch (err) {
      error.value = err.message
      throw err
    } finally {
      loading.value = false
    }
  }

  return {
    items,
    currentDetail,
    loading,
    error,
    fetchList,
    fetchByExternalId,
    createMember,
    updateMember,
    importScopusPublications,
  }
})
