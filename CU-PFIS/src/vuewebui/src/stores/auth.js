import { defineStore } from 'pinia'
import { computed, ref } from 'vue'
import { httpClient, setAuthHeaderProvider } from '@/api/httpClient'
import { updateAbilityForPermissions } from '@/plugins/casl'

const PERSONA_STORAGE_KEY = 'cu-pfis-demo-persona'

export const PDPA_POLICY_VERSION = '2026.1'

export const DEMO_PERSONAS = {
  admin: {
    id: 'admin',
    userId: 'dev-admin',
    loginName: 'admin.pfis',
    displayNameTh: 'ผู้ดูแลระบบ (Admin คณะ)',
    displayNameEn: 'System Administrator (Faculty)',
    role: 'Admin',
    departmentCode: null,
    departmentNameTh: 'สำนักงานคณะ (ทุกภาควิชา)',
    departmentNameEn: 'Faculty Office (All Departments)',
    facultyPersonnelCode: null,
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
    userId: 'dev-staff-pp',
    loginName: 'staff.pharmcare',
    displayNameTh: 'น.ส.สุภาวดี วิชาการ (เจ้าหน้าที่ภาควิชาเภสัชกรรมปฏิบัติ)',
    displayNameEn: 'Ms. Supawadee Wichakarn (Dept. Staff — Pharmacy Practice)',
    role: 'Staff',
    departmentCode: 'PP',
    departmentNameTh: 'ภาควิชาเภสัชกรรมปฏิบัติ',
    departmentNameEn: 'Department of Pharmacy Practice',
    facultyPersonnelCode: null,
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
    userId: 'dev-faculty-somchai',
    loginName: 'somchai.p',
    displayNameTh: 'รศ.ดร.ภก.สมชาย เภสัชศาสตร์ (อาจารย์)',
    displayNameEn: 'Assoc. Prof. Dr. Somchai Phesatchasat (Faculty)',
    role: 'Faculty',
    departmentCode: 'PP',
    departmentNameTh: 'ภาควิชาเภสัชกรรมปฏิบัติ',
    departmentNameEn: 'Department of Pharmacy Practice',
    facultyPersonnelCode: 'PHARM-1001',
    permissions: [
      'reference-data.view',
      'faculty-cv.view-own',
      'faculty-cv.edit-own',
      'faculty-cv.export-own',
    ],
  },
}

export const useAuthStore = defineStore('auth', () => {
  const personaKey = ref(localStorage.getItem(PERSONA_STORAGE_KEY) || 'admin')
  const pdpaStatus = ref({ hasAccepted: false, policyVersion: PDPA_POLICY_VERSION, acceptedAt: null })
  const pdpaLoaded = ref(false)
  const resolvedDepartmentExternalId = ref(null)
  const resolvedFacultyExternalId = ref(null)
  const apiError = ref(null)

  const currentUser = computed(() => DEMO_PERSONAS[personaKey.value] || DEMO_PERSONAS.admin)
  const hasAcceptedPdpa = computed(() => Boolean(pdpaStatus.value?.hasAccepted))
  const pdpaRecord = computed(() => (hasAcceptedPdpa.value ? pdpaStatus.value : null))

  const isAdmin = computed(() => currentUser.value.role === 'Admin')
  const isStaff = computed(() => currentUser.value.role === 'Staff')
  const isFaculty = computed(() => currentUser.value.role === 'Faculty')

  // Wire live Dev Auth headers into httpClient (maps to DevelopmentHeaderAuthMiddleware in CU-PFIS API)
  setAuthHeaderProvider(() => {
    const user = currentUser.value
    const headers = {
      'X-Dev-Role': user.role,
      'X-Dev-User-Id': user.userId,
      'X-Dev-Login': user.loginName,
    }
    if (user.role === 'Staff' && resolvedDepartmentExternalId.value) {
      headers['X-Dev-Department-Id'] = resolvedDepartmentExternalId.value
    }
    if (user.role === 'Faculty' && resolvedFacultyExternalId.value) {
      headers['X-Dev-Faculty-Id'] = resolvedFacultyExternalId.value
    }
    return headers
  })

  function syncAbility() {
    updateAbilityForPermissions(currentUser.value.permissions)
  }

  // Resolve real seeded UUIDv7s for Staff dept & Faculty own-CV from the live PostgreSQL backend
  async function resolveLiveScopeIds() {
    const user = currentUser.value
    resolvedDepartmentExternalId.value = null
    resolvedFacultyExternalId.value = null

    if (user.role === 'Admin') return

    try {
      // Temporarily read reference/faculty lookup as Admin to map demo code -> real DB ExternalId
      const adminHeaders = {
        'X-Dev-Role': 'Admin',
        'X-Dev-User-Id': 'dev-admin',
        'X-Dev-Login': 'admin.pfis',
      }

      if (user.departmentCode) {
        const res = await fetch('/api/departments?activeOnly=true', { headers: adminHeaders })
        if (res.ok) {
          const depts = await res.json()
          const matched = depts.find((d) => d.code === user.departmentCode)
          if (matched) resolvedDepartmentExternalId.value = matched.externalId
        }
      }

      if (user.facultyPersonnelCode) {
        const res = await fetch(`/api/faculty-members?search=${encodeURIComponent(user.facultyPersonnelCode)}`, {
          headers: adminHeaders,
        })
        if (res.ok) {
          const list = await res.json()
          const matched = list.find((f) => f.personnelCode === user.facultyPersonnelCode)
          if (matched) resolvedFacultyExternalId.value = matched.externalId
        }
      }
    } catch {
      // Handled by main API error banner
    }
  }

  async function fetchPdpaStatus() {
    try {
      apiError.value = null
      const status = await httpClient.getMyPdpaConsent(PDPA_POLICY_VERSION)
      pdpaStatus.value = status
    } catch (err) {
      apiError.value = err.message
      pdpaStatus.value = { hasAccepted: false, policyVersion: PDPA_POLICY_VERSION, acceptedAt: null }
    } finally {
      pdpaLoaded.value = true
    }
  }

  async function acceptPdpa() {
    apiError.value = null
    const status = await httpClient.recordPdpaConsent(PDPA_POLICY_VERSION)
    pdpaStatus.value = status
    pdpaLoaded.value = true
    return status
  }

  async function setPersona(key) {
    if (!DEMO_PERSONAS[key]) return
    personaKey.value = key
    localStorage.setItem(PERSONA_STORAGE_KEY, key)
    syncAbility()
    await resolveLiveScopeIds()
    await fetchPdpaStatus()
  }

  function resetPdpaLocalPreview() {
    // Allows previewing the PDPA screen again in UI (actual DB consents are append-only!)
    pdpaStatus.value = { hasAccepted: false, policyVersion: PDPA_POLICY_VERSION, acceptedAt: null }
  }

  return {
    personaKey,
    currentUser,
    pdpaRecord,
    pdpaLoaded,
    hasAcceptedPdpa,
    resolvedDepartmentExternalId,
    resolvedFacultyExternalId,
    apiError,
    isAdmin,
    isStaff,
    isFaculty,
    syncAbility,
    resolveLiveScopeIds,
    fetchPdpaStatus,
    acceptPdpa,
    setPersona,
    resetPdpaLocalPreview,
  }
})
