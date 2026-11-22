import { defineStore } from 'pinia'
import { ref } from 'vue'

function createExternalId() {
  if (typeof crypto !== 'undefined' && crypto.randomUUID) {
    return crypto.randomUUID()
  }
  return `0195a001-${Date.now()}-7000-8000-000000000099`
}

export const useFacultyMembersStore = defineStore('facultyMembers', () => {
  const items = ref([
    {
      externalId: '0195a001-0001-7000-8000-000000000001',
      personnelCode: 'PHARM-1001',
      professionalTitleTh: 'ดร.ภก.',
      professionalTitleEn: 'Dr. Pharm.',
      firstNameTh: 'สมชาย',
      lastNameTh: 'เภสัชศาสตร์',
      firstNameEn: 'Somchai',
      lastNameEn: 'Phesatchasat',
      academicPositionExternalId: 'pos-assoc',
      administrativePositionsTh: 'รองคณบดีฝ่ายวิชาการ',
      administrativePositionsEn: 'Associate Dean for Academic Affairs',
      departmentExternalId: 'dept-pharmacy-practice',
      email: 'somchai.p@pharm.chula.ac.th',
      phoneNumber: '02-218-8401',
      scopusAuthorId: '57191234501',
      orcidId: '0000-0002-1825-0097',
      lastUpdatedAt: '2026-11-12',
      educations: [
        {
          degreeTh: 'ปร.ด. (เภสัชกรรมคลินิก)',
          degreeEn: 'Ph.D. (Clinical Pharmacy)',
          institutionTh: 'จุฬาลงกรณ์มหาวิทยาลัย',
          institutionEn: 'Chulalongkorn University',
          graduationYear: 2016,
        },
        {
          degreeTh: 'ภ.บ. (เกียรตินิยมอันดับ 1)',
          degreeEn: 'B.Sc. in Pharmacy (1st Class Honors)',
          institutionTh: 'จุฬาลงกรณ์มหาวิทยาลัย',
          institutionEn: 'Chulalongkorn University',
          graduationYear: 2010,
        },
      ],
      publications: [
        {
          title: 'Clinical Pharmacist Interventions in Antimicrobial Stewardship Across Thai University Hospitals',
          journalOrVenue: 'International Journal of Clinical Pharmacy',
          publishedYear: 2025,
          doi: '10.1007/s11096-025-01842-x',
          scopusId: '2-s2.0-85210045123',
        },
        {
          title: 'Telepharmacy Outcomes for Chronic Disease Management in Urban Bangkok Communities',
          journalOrVenue: 'Research in Social and Administrative Pharmacy',
          publishedYear: 2024,
          doi: '10.1016/j.sapharm.2024.06.011',
          scopusId: '2-s2.0-85199012456',
        },
      ],
      approvedTrainings: [
        {
          titleTh: 'การประชุมวิชาการเภสัชกรรมคลินิกระดับชาติ ประจำปี 2569',
          titleEn: 'National Clinical Pharmacy Conference 2026',
          workTypeTh: 'วิชาการ',
          workTypeEn: 'Academic',
          categoryTh: 'ใช้เงินวิชาการ',
          categoryEn: 'Academic Funded',
          dateRange: '2026-08-14 – 2026-08-16',
        },
        {
          titleTh: 'สัมมนาคณาจารย์คณะเภสัชศาสตร์ ประจำปีการศึกษา 2568',
          titleEn: 'Annual Faculty Retreat & Curriculum Seminar 2025',
          workTypeTh: 'วิชาการ',
          workTypeEn: 'Academic',
          categoryTh: 'คณะจัด',
          categoryEn: 'Faculty Organized',
          dateRange: '2026-05-20 – 2026-05-21',
        },
      ],
    },
    {
      externalId: '0195a001-0002-7000-8000-000000000002',
      personnelCode: 'PHARM-1002',
      professionalTitleTh: 'ดร.ภญ.',
      professionalTitleEn: 'Dr. Pharm.',
      firstNameTh: 'นภาวรรณ',
      lastNameTh: 'วิทยาการยา',
      firstNameEn: 'Napawan',
      lastNameEn: 'Wittayakanya',
      academicPositionExternalId: 'pos-prof',
      administrativePositionsTh: 'หัวหน้าภาควิชาวิทยาการเภสัชกรรมและเภสัชอุตสาหกรรม',
      administrativePositionsEn: 'Head, Department of Pharmaceutics and Industrial Pharmacy',
      departmentExternalId: 'dept-pharmaceutics',
      email: 'napawan.w@pharm.chula.ac.th',
      phoneNumber: '02-218-8392',
      scopusAuthorId: '57191234502',
      orcidId: '0000-0003-2190-4412',
      lastUpdatedAt: '2026-11-18',
      educations: [
        {
          degreeTh: 'Ph.D. (Pharmaceutics)',
          degreeEn: 'Ph.D. (Pharmaceutics)',
          institutionTh: 'University of Wisconsin-Madison, USA',
          institutionEn: 'University of Wisconsin-Madison, USA',
          graduationYear: 2012,
        },
      ],
      publications: [
        {
          title: 'Lipid Nanoparticle Formulation Optimization for mRNA Stability under Tropical Storage',
          journalOrVenue: 'Journal of Controlled Release',
          publishedYear: 2026,
          doi: '10.1016/j.jconrel.2026.01.008',
          scopusId: '2-s2.0-85231009877',
        },
      ],
      approvedTrainings: [
        {
          titleTh: 'AAPS PharmSci 360 International Symposium',
          titleEn: 'AAPS PharmSci 360 International Symposium',
          workTypeTh: 'วิจัย',
          workTypeEn: 'Research',
          categoryTh: 'อบรมระยะสั้น / ต่างประเทศ',
          categoryEn: 'Short-Term / Overseas',
          dateRange: '2026-10-18 – 2026-10-22',
        },
      ],
    },
    {
      externalId: '0195a001-0003-7000-8000-000000000003',
      personnelCode: 'PHARM-1003',
      professionalTitleTh: 'ภญ.',
      professionalTitleEn: 'Pharm.',
      firstNameTh: 'พิมพ์ชนก',
      lastNameTh: 'บริบาลคลินิก',
      firstNameEn: 'Pimchanok',
      lastNameEn: 'Boribanclinic',
      academicPositionExternalId: 'pos-asst',
      administrativePositionsTh: 'กรรมการหลักสูตรเภสัชศาสตรบัณฑิต',
      administrativePositionsEn: 'Pharm.D. Curriculum Committee Member',
      departmentExternalId: 'dept-pharmacy-practice',
      email: 'pimchanok.b@pharm.chula.ac.th',
      phoneNumber: '02-218-8415',
      scopusAuthorId: '57204451980',
      orcidId: '',
      lastUpdatedAt: '2026-10-30',
      educations: [
        {
          degreeTh: 'ภ.ม. (เภสัชกรรมคลินิก)',
          degreeEn: 'M.Pharm. (Clinical Pharmacy)',
          institutionTh: 'จุฬาลงกรณ์มหาวิทยาลัย',
          institutionEn: 'Chulalongkorn University',
          graduationYear: 2019,
        },
      ],
      publications: [
        {
          title: 'Medication Reconciliation Models in Geriatric Outpatient Clinics',
          journalOrVenue: 'Thai Journal of Pharmaceutical Sciences',
          publishedYear: 2025,
          doi: '10.56808/3027-7922.2901',
          scopusId: '2-s2.0-85220112233',
        },
      ],
      approvedTrainings: [],
    },
  ])

  function getByExternalId(externalId) {
    return items.value.find((item) => item.externalId === externalId)
  }

  function createMember(payload) {
    const newItem = {
      ...payload,
      externalId: createExternalId(),
      lastUpdatedAt: new Date().toISOString().slice(0, 10),
      approvedTrainings: [],
    }
    items.value.unshift(newItem)
    return newItem
  }

  function updateMember(externalId, payload) {
    const index = items.value.findIndex((item) => item.externalId === externalId)
    if (index === -1) return null
    items.value[index] = {
      ...items.value[index],
      ...payload,
      externalId,
      lastUpdatedAt: new Date().toISOString().slice(0, 10),
    }
    return items.value[index]
  }

  // A16 Phase 1 fallback: deduplicate Scopus CSV imports by EID or DOI
  function importScopusPublications(externalId, incomingPublications) {
    const member = getByExternalId(externalId)
    if (!member) return { added: 0, skipped: 0 }

    let added = 0
    let skipped = 0

    for (const pub of incomingPublications) {
      const exists = member.publications.some(
        (existing) =>
          (pub.scopusId && existing.scopusId && existing.scopusId === pub.scopusId) ||
          (pub.doi && existing.doi && existing.doi.toLowerCase() === pub.doi.toLowerCase()),
      )

      if (exists) {
        skipped += 1
      } else {
        member.publications.unshift(pub)
        added += 1
      }
    }

    member.lastUpdatedAt = new Date().toISOString().slice(0, 10)
    return { added, skipped }
  }

  return {
    items,
    getByExternalId,
    createMember,
    updateMember,
    importScopusPublications,
  }
})
