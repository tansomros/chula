import { defineStore } from 'pinia'
import { computed, ref } from 'vue'
import { updateAbilityForPermissions } from '@/plugins/casl'

const PDPA_STORAGE_KEY = 'cu-pfis-pdpa-consent'
const PERSONA_STORAGE_KEY = 'cu-pfis-demo-persona'

export const PDPA_POLICY_VERSION = '2026.1'

export const DEMO_PERSONAS = {
  admin: {
    id: 'admin',
    loginName: 'admin.pfis',
    displayNameTh: 'ผู้ดูแลระบบ (Admin คณะ)',
    displayNameEn: 'System Administrator (Faculty)',
    role: 'Admin',
    departmentExternalId: null,
    departmentNameTh: 'สำนักงานคณะ (ทุกภาควิชา)',
    departmentNameEn: 'Faculty Office (All Departments)',
    facultyExternalId: null,
    permissions: [
      'reference-data.view',
      'reference-data.manage',
      'faculty-cv.view-all',
      'faculty-cv.edit-all',
      'faculty-cv.export-all',
      'faculty-cv.notify-update',
      'students-cv.view',
      'students-cv.edit',
      'students-cv.export',
      'employer-evaluations.view',
    ],
  },
  staffDept: {
    id: 'staffDept',
    loginName: 'staff.pharmcare',
    displayNameTh: 'น.ส.สุภาวดี วิชาการ (เจ้าหน้าที่ภาควิชาเภสัชกรรมปฏิบัติ)',
    displayNameEn: 'Ms. Supawadee Wichakarn (Dept. Staff — Pharmacy Practice)',
    role: 'Staff',
    departmentExternalId: 'dept-pharmacy-practice',
    departmentNameTh: 'ภาควิชาเภสัชกรรมปฏิบัติ',
    departmentNameEn: 'Department of Pharmacy Practice',
    facultyExternalId: null,
    permissions: [
      'reference-data.view',
      'faculty-cv.view-all',
      'faculty-cv.edit-all',
      'faculty-cv.export-all',
      'faculty-cv.notify-update',
      'students-cv.view',
      'students-cv.edit',
      'students-cv.export',
      'employer-evaluations.view',
    ],
  },
  faculty: {
    id: 'faculty',
    loginName: 'somchai.p',
    displayNameTh: 'รศ.ดร.ภก.สมชาย เภสัชศาสตร์ (อาจารย์)',
    displayNameEn: 'Assoc. Prof. Dr. Somchai Phesatchasat (Faculty)',
    role: 'Faculty',
    departmentExternalId: 'dept-pharmacy-practice',
    departmentNameTh: 'ภาควิชาเภสัชกรรมปฏิบัติ',
    departmentNameEn: 'Department of Pharmacy Practice',
    facultyExternalId: '0195a001-0001-7000-8000-000000000001',
    permissions: [
      'reference-data.view',
      'faculty-cv.view-own',
      'faculty-cv.edit-own',
      'faculty-cv.export-own',
    ],
  },
}

function loadSavedPdpa() {
  try {
    const raw = localStorage.getItem(PDPA_STORAGE_KEY)
    return raw ? JSON.parse(raw) : null
  } catch {
    return null
  }
}

export const useAuthStore = defineStore('auth', () => {
  const personaKey = ref(localStorage.getItem(PERSONA_STORAGE_KEY) || 'admin')
  const pdpaRecord = ref(loadSavedPdpa())

  const currentUser = computed(() => DEMO_PERSONAS[personaKey.value] || DEMO_PERSONAS.admin)

  const hasAcceptedPdpa = computed(
    () => Boolean(pdpaRecord.value && pdpaRecord.value.policyVersion === PDPA_POLICY_VERSION),
  )

  const isAdmin = computed(() => currentUser.value.role === 'Admin')
  const isStaff = computed(() => currentUser.value.role === 'Staff')
  const isFaculty = computed(() => currentUser.value.role === 'Faculty')

  function syncAbility() {
    updateAbilityForPermissions(currentUser.value.permissions)
  }

  function setPersona(key) {
    if (!DEMO_PERSONAS[key]) return
    personaKey.value = key
    localStorage.setItem(PERSONA_STORAGE_KEY, key)
    syncAbility()
  }

  function acceptPdpa() {
    const record = {
      loginName: currentUser.value.loginName,
      policyVersion: PDPA_POLICY_VERSION,
      acceptedAt: new Date().toISOString(),
    }
    pdpaRecord.value = record
    localStorage.setItem(PDPA_STORAGE_KEY, JSON.stringify(record))
    return record
  }

  function resetPdpa() {
    pdpaRecord.value = null
    localStorage.removeItem(PDPA_STORAGE_KEY)
  }

  function canAccessFaculty(facultyExternalId, departmentExternalId) {
    if (isAdmin.value) return true
    if (isStaff.value) {
      return !currentUser.value.departmentExternalId || currentUser.value.departmentExternalId === departmentExternalId
    }
    if (isFaculty.value) {
      return currentUser.value.facultyExternalId === facultyExternalId
    }
    return false
  }

  return {
    personaKey,
    currentUser,
    pdpaRecord,
    hasAcceptedPdpa,
    isAdmin,
    isStaff,
    isFaculty,
    syncAbility,
    setPersona,
    acceptPdpa,
    resetPdpa,
    canAccessFaculty,
  }
})
