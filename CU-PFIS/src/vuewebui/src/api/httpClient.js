const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || '/api'

let headerProvider = () => ({})

export function setAuthHeaderProvider(provider) {
  headerProvider = provider
}

function buildQuery(params = {}) {
  const searchParams = new URLSearchParams()
  for (const [key, value] of Object.entries(params)) {
    if (value !== null && value !== undefined && value !== '') {
      searchParams.set(key, String(value))
    }
  }
  const query = searchParams.toString()
  return query ? `?${query}` : ''
}

async function request(path, options = {}) {
  const authHeaders = headerProvider() || {}
  const response = await fetch(`${API_BASE_URL}${path}`, {
    ...options,
    headers: {
      'Content-Type': 'application/json',
      ...authHeaders,
      ...(options.headers || {}),
    },
  })

  if (!response.ok) {
    let errorMessage = `HTTP ${response.status}`
    try {
      const data = await response.json()
      errorMessage = data.detail || data.title || JSON.stringify(data.errors || data)
    } catch {
      const text = await response.text()
      if (text) errorMessage = text
    }
    const err = new Error(errorMessage)
    err.status = response.status
    throw err
  }

  if (response.status === 204) {
    return null
  }

  return response.json()
}

// Standardized lower-kebab-case HTTP client aligned with CU-PFIS ASP.NET Core routes
export const httpClient = {
  // Security — PDPA Consent (A18)
  getMyPdpaConsent: (policyVersion = '2026.1') =>
    request(`/pdpa-consents/me${buildQuery({ policyVersion })}`),
  recordPdpaConsent: (policyVersion = '2026.1') =>
    request('/pdpa-consents', {
      method: 'POST',
      body: JSON.stringify({ policyVersion }),
    }),

  // Reference Data
  getAcademicYears: (activeOnly = true) =>
    request(`/academic-years${buildQuery({ activeOnly })}`),
  getDepartments: (activeOnly = true) =>
    request(`/departments${buildQuery({ activeOnly })}`),
  getMajors: (activeOnly = true) =>
    request(`/majors${buildQuery({ activeOnly })}`),
  getAcademicPositions: (activeOnly = true) =>
    request(`/academic-positions${buildQuery({ activeOnly })}`),
  getAdmissionRounds: (activeOnly = true) =>
    request(`/admission-rounds${buildQuery({ activeOnly })}`),

  // Faculty CV (Phase 1)
  getFacultyMembers: (filters = {}) =>
    request(`/faculty-members${buildQuery(filters)}`),
  getFacultyMember: (externalId) =>
    request(`/faculty-members/${externalId}`),
  createFacultyMember: (payload) =>
    request('/faculty-members', {
      method: 'POST',
      body: JSON.stringify(payload),
    }),
  updateFacultyMember: (externalId, payload) =>
    request(`/faculty-members/${externalId}`, {
      method: 'PUT',
      body: JSON.stringify(payload),
    }),
  importScopusPublications: (externalId, publications, sourceSystem = 'ScopusCsv') =>
    request(`/faculty-members/${externalId}/import-scopus`, {
      method: 'POST',
      body: JSON.stringify({
        facultyMemberExternalId: externalId,
        publications,
        sourceSystem,
      }),
    }),

  // Student CV (Phase 1)
  getStudents: (filters = {}) =>
    request(`/students${buildQuery(filters)}`),
  getStudent: (externalId) =>
    request(`/students/${externalId}`),
  createStudent: (payload) =>
    request('/students', {
      method: 'POST',
      body: JSON.stringify(payload),
    }),
  updateStudent: (externalId, payload) =>
    request(`/students/${externalId}`, {
      method: 'PUT',
      body: JSON.stringify(payload),
    }),

  // CU-PPEP Integration & Non-Destructive Sync (D11 / P01)
  getCuppepHealth: () =>
    request('/integrations/cuppep/health'),
  syncCuppepStudents: (payload = {}) =>
    request('/integrations/cuppep/sync-students', {
      method: 'POST',
      body: JSON.stringify(payload),
    }),
  getImportBatches: (take = 20) =>
    request(`/integrations/cuppep/batches${buildQuery({ take })}`),
  getDataConflicts: (status = 'Pending') =>
    request(`/integrations/cuppep/conflicts${buildQuery({ status })}`),
  resolveDataConflict: (externalId, action, note = null) =>
    request(`/integrations/cuppep/conflicts/${externalId}/resolve`, {
      method: 'POST',
      body: JSON.stringify({ action, note }),
    }),
}
