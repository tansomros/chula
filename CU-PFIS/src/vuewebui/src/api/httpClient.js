const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || '/api'

async function request(path, options = {}) {
  const response = await fetch(`${API_BASE_URL}${path}`, {
    headers: {
      'Content-Type': 'application/json',
      ...(options.headers || {}),
    },
    ...options,
  })

  if (!response.ok) {
    const message = await response.text()
    throw new Error(message || `Request failed with status ${response.status}`)
  }

  if (response.status === 204) {
    return null
  }

  return response.json()
}

// Standardized lower-kebab-case API client aligned with CU-PFIS ASP.NET Core routes
export const httpClient = {
  getAcademicYears: (activeOnly = true) => request(`/academic-years?activeOnly=${activeOnly}`),
  getFacultyMembers: () => request('/faculty-members'),
  getFacultyMember: (externalId) => request(`/faculty-members/${externalId}`),
  createFacultyMember: (payload) =>
    request('/faculty-members', { method: 'POST', body: JSON.stringify(payload) }),
  updateFacultyMember: (externalId, payload) =>
    request(`/faculty-members/${externalId}`, { method: 'PUT', body: JSON.stringify(payload) }),
  getStudents: () => request('/students'),
  getStudent: (externalId) => request(`/students/${externalId}`),
  submitPdpaConsent: (payload) =>
    request('/pdpa-consents', { method: 'POST', body: JSON.stringify(payload) }),
}
