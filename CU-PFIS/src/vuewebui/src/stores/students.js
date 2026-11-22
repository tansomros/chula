import { defineStore } from 'pinia'
import { ref } from 'vue'
import { httpClient } from '@/api/httpClient'

export const useStudentsStore = defineStore('students', () => {
  const items = ref([])
  const currentDetail = ref(null)
  const loading = ref(false)
  const error = ref(null)

  async function fetchList(filters = {}) {
    loading.value = true
    error.value = null
    try {
      items.value = await httpClient.getStudents(filters)
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
      currentDetail.value = await httpClient.getStudent(externalId)
      return currentDetail.value
    } catch (err) {
      error.value = err.message
      return null
    } finally {
      loading.value = false
    }
  }

  async function updateStudent(externalId, payload) {
    loading.value = true
    error.value = null
    try {
      await httpClient.updateStudent(externalId, { ...payload, externalId })
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
    updateStudent,
  }
})
