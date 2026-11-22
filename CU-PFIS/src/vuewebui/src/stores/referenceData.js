import { defineStore } from 'pinia'
import { ref } from 'vue'
import { httpClient } from '@/api/httpClient'

export const useReferenceDataStore = defineStore('referenceData', () => {
  const academicYears = ref([])
  const departments = ref([])
  const majors = ref([])
  const academicPositions = ref([])
  const admissionRounds = ref([])
  const loaded = ref(false)
  const loading = ref(false)
  const error = ref(null)

  async function ensureLoaded(force = false) {
    if (loaded.value && !force) return
    loading.value = true
    error.value = null
    try {
      const [years, depts, majorList, positions, rounds] = await Promise.all([
        httpClient.getAcademicYears(true),
        httpClient.getDepartments(true),
        httpClient.getMajors(true),
        httpClient.getAcademicPositions(true),
        httpClient.getAdmissionRounds(true),
      ])

      academicYears.value = years.map((y) => ({
        ...y,
        labelTh: `ปีการศึกษา ${y.yearBe}`,
        labelEn: `Academic Year ${y.yearCe} (${y.yearBe})`,
      }))
      departments.value = depts
      majors.value = majorList
      academicPositions.value = positions
      admissionRounds.value = rounds
      loaded.value = true
    } catch (err) {
      error.value = err.message
    } finally {
      loading.value = false
    }
  }

  function getDepartmentById(id) {
    return departments.value.find((d) => d.externalId === id)
  }

  function getMajorById(id) {
    return majors.value.find((m) => m.externalId === id)
  }

  function getAcademicPositionById(id) {
    return academicPositions.value.find((p) => p.externalId === id)
  }

  function getParentAdmissionRounds() {
    return admissionRounds.value.filter((r) => !r.parentExternalId)
  }

  function getChildAdmissionRounds(parentId) {
    return admissionRounds.value.filter((r) => r.parentExternalId === parentId)
  }

  function getAdmissionRoundLabel(roundId, locale = 'th') {
    const item = admissionRounds.value.find((r) => r.externalId === roundId)
    if (!item) return '-'
    const label = locale === 'en' ? item.nameEn : item.nameTh
    if (!item.parentExternalId) return label
    const parent = admissionRounds.value.find((r) => r.externalId === item.parentExternalId)
    if (!parent) return label
    return `${locale === 'en' ? parent.nameEn : parent.nameTh} › ${label}`
  }

  return {
    academicYears,
    departments,
    majors,
    academicPositions,
    admissionRounds,
    loaded,
    loading,
    error,
    ensureLoaded,
    getDepartmentById,
    getMajorById,
    getAcademicPositionById,
    getParentAdmissionRounds,
    getChildAdmissionRounds,
    getAdmissionRoundLabel,
  }
})
