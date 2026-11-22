import { defineStore } from 'pinia'
import { ref } from 'vue'

export const useReferenceDataStore = defineStore('referenceData', () => {
  // 10+ academic years (2560–2569 BE)
  const academicYears = ref(
    Array.from({ length: 10 }, (_, idx) => {
      const yearBe = 2569 - idx
      const yearCe = yearBe - 543
      return {
        externalId: `ay-${yearBe}`,
        yearBe,
        yearCe,
        labelTh: `ปีการศึกษา ${yearBe}`,
        labelEn: `Academic Year ${yearCe} (${yearBe})`,
        isCurrent: yearBe === 2569,
      }
    }),
  )

  const departments = ref([
    {
      externalId: 'dept-pharmacy-practice',
      code: 'PP',
      nameTh: 'ภาควิชาเภสัชกรรมปฏิบัติ',
      nameEn: 'Department of Pharmacy Practice',
    },
    {
      externalId: 'dept-pharmaceutics',
      code: 'PI',
      nameTh: 'ภาควิชาวิทยาการเภสัชกรรมและเภสัชอุตสาหกรรม',
      nameEn: 'Department of Pharmaceutics and Industrial Pharmacy',
    },
    {
      externalId: 'dept-pharmacology',
      code: 'PH',
      nameTh: 'ภาควิชาเภสัชวิทยาและสรีรวิทยา',
      nameEn: 'Department of Pharmacology and Physiology',
    },
    {
      externalId: 'dept-pharmacognosy',
      code: 'PG',
      nameTh: 'ภาควิชาเภสัชเวทและเภสัชพฤกษศาสตร์',
      nameEn: 'Department of Pharmacognosy and Pharmaceutical Botany',
    },
    {
      externalId: 'dept-social-admin',
      code: 'SA',
      nameTh: 'ภาควิชาเภสัชกรรมสังคมและบริหาร',
      nameEn: 'Department of Social and Administrative Pharmacy',
    },
  ])

  const majors = ref([
    {
      externalId: 'major-pc',
      code: 'PC',
      nameTh: 'สาขาวิชาการบริบาลทางเภสัชกรรม',
      nameEn: 'Pharmaceutical Care',
    },
    {
      externalId: 'major-ip',
      code: 'IP',
      nameTh: 'สาขาวิชาเภสัชกรรมอุตสาหการ',
      nameEn: 'Industrial Pharmacy',
    },
  ])

  const academicPositions = ref([
    { externalId: 'pos-prof', code: 'PROF', titleTh: 'ศาสตราจารย์ (ศ.)', titleEn: 'Professor (Prof.)' },
    { externalId: 'pos-assoc', code: 'ASSOC', titleTh: 'รองศาสตราจารย์ (รศ.)', titleEn: 'Associate Professor (Assoc. Prof.)' },
    { externalId: 'pos-asst', code: 'ASST', titleTh: 'ผู้ช่วยศาสตราจารย์ (ผศ.)', titleEn: 'Assistant Professor (Asst. Prof.)' },
    { externalId: 'pos-lect', code: 'LECT', titleTh: 'อาจารย์ (อ.)', titleEn: 'Lecturer' },
  ])

  // 2-tier Admission Round hierarchy per req071169.pdf pages 4–5
  const admissionRounds = ref([
    {
      externalId: 'round-portfolio',
      parentExternalId: null,
      tier: 1,
      nameTh: 'รอบ Portfolio',
      nameEn: 'Portfolio Round',
    },
    {
      externalId: 'round-portfolio-olympiad',
      parentExternalId: 'round-portfolio',
      tier: 2,
      nameTh: 'Port โอลิมปิกวิชาการ',
      nameEn: 'Portfolio — Academic Olympiad',
    },
    {
      externalId: 'round-portfolio-english',
      parentExternalId: 'round-portfolio',
      tier: 2,
      nameTh: 'Port ภาษาอังกฤษ',
      nameEn: 'Portfolio — English Proficiency',
    },
    {
      externalId: 'round-quota',
      parentExternalId: null,
      tier: 1,
      nameTh: 'รอบ Quota',
      nameEn: 'Quota Round',
    },
    {
      externalId: 'round-quota-satit',
      parentExternalId: 'round-quota',
      tier: 2,
      nameTh: 'Quota สาธิตจุฬาฯ',
      nameEn: 'Quota — Chulalongkorn Demonstration School',
    },
    {
      externalId: 'round-quota-sport',
      parentExternalId: 'round-quota',
      tier: 2,
      nameTh: 'Quota กีฬาชาติ',
      nameEn: 'Quota — National Athlete',
    },
    {
      externalId: 'round-quota-rural',
      parentExternalId: 'round-quota',
      tier: 2,
      nameTh: 'Quota จุฬาชนบท',
      nameEn: 'Quota — Chula Rural',
    },
    {
      externalId: 'round-quota-interior',
      parentExternalId: 'round-quota',
      tier: 2,
      nameTh: 'Quota มหาดไทย',
      nameEn: 'Quota — Ministry of Interior',
    },
    {
      externalId: 'round-quota-kvis',
      parentExternalId: 'round-quota',
      tier: 2,
      nameTh: 'Quota กำเนิดวิทย์',
      nameEn: 'Quota — Kamnoetvidya (KVIS)',
    },
    {
      externalId: 'round-cotmes',
      parentExternalId: null,
      tier: 1,
      nameTh: 'รอบ กสพท.',
      nameEn: 'COTMES / National Admission Round',
    },
  ])

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
    return admissionRounds.value.filter((r) => r.tier === 1)
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
    getDepartmentById,
    getMajorById,
    getAcademicPositionById,
    getParentAdmissionRounds,
    getChildAdmissionRounds,
    getAdmissionRoundLabel,
  }
})
