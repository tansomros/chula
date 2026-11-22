<script setup>
import { computed, onMounted, reactive, ref, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { useRoute, useRouter } from 'vue-router'
import { useAuthStore } from '@/stores/auth'
import { useStudentsStore } from '@/stores/students'
import { useFacultyMembersStore } from '@/stores/facultyMembers'
import { useReferenceDataStore } from '@/stores/referenceData'
import { useLocalizedField } from '@/composables/useLocalizedField'

const { t } = useI18n()
const route = useRoute()
const router = useRouter()
const authStore = useAuthStore()
const studentsStore = useStudentsStore()
const facultyStore = useFacultyMembersStore()
const refStore = useReferenceDataStore()
const { locale, formatFullName } = useLocalizedField()

const student = computed(() => studentsStore.currentDetail)
const formRef = ref(null)
const submitting = ref(false)
const errorMessage = ref('')
const isHydrating = ref(false)

const form = reactive({
  firstNameTh: '',
  lastNameTh: '',
  firstNameEn: '',
  lastNameEn: '',
  studyYear: 1,
  majorExternalId: null,
  departmentExternalId: null,
  academicAdvisorExternalId: null,
  parentAdmissionRoundId: null,
  childAdmissionRoundId: null,
  highSchoolTh: '',
  highSchoolEn: '',
})

const majorItems = computed(() =>
  refStore.majors.map((m) => ({
    value: m.externalId,
    title: locale.value === 'en' ? m.nameEn : m.nameTh,
  })),
)

const departmentItems = computed(() =>
  refStore.departments.map((d) => ({
    value: d.externalId,
    title: locale.value === 'en' ? d.nameEn : d.nameTh,
  })),
)

const advisorItems = computed(() =>
  facultyStore.items.map((f) => ({
    value: f.externalId,
    title: formatFullName(f, true),
  })),
)

const parentRoundItems = computed(() =>
  refStore.getParentAdmissionRounds().map((r) => ({
    value: r.externalId,
    title: locale.value === 'en' ? r.nameEn : r.nameTh,
  })),
)

// 2-tier cascading dropdown for Admission Round (Portfolio / Quota have sub-programs; กสพท. is single-tier)
const childRoundItems = computed(() =>
  form.parentAdmissionRoundId
    ? refStore.getChildAdmissionRounds(form.parentAdmissionRoundId).map((r) => ({
        value: r.externalId,
        title: locale.value === 'en' ? r.nameEn : r.nameTh,
      }))
    : [],
)

function hydrateForm(detail) {
  if (!detail) return
  isHydrating.value = true
  form.firstNameTh = detail.firstNameTh || ''
  form.lastNameTh = detail.lastNameTh || ''
  form.firstNameEn = detail.firstNameEn || ''
  form.lastNameEn = detail.lastNameEn || ''
  form.studyYear = detail.studyYear || 1
  form.majorExternalId = detail.majorExternalId || null
  form.departmentExternalId = detail.departmentExternalId || null
  form.academicAdvisorExternalId = detail.academicAdvisorExternalId || null
  form.parentAdmissionRoundId = detail.parentAdmissionRoundExternalId || detail.admissionRoundExternalId || null
  form.childAdmissionRoundId = detail.parentAdmissionRoundExternalId ? detail.admissionRoundExternalId : null
  form.highSchoolTh = detail.highSchoolTh || ''
  form.highSchoolEn = detail.highSchoolEn || ''
  isHydrating.value = false
}

watch(
  () => form.parentAdmissionRoundId,
  () => {
    if (isHydrating.value) return
    const validChildren = childRoundItems.value.map((c) => c.value)
    if (!validChildren.includes(form.childAdmissionRoundId)) {
      form.childAdmissionRoundId = validChildren[0] || null
    }
  },
)

async function loadData() {
  if (!route.params.id) return
  const [, detail] = await Promise.all([
    refStore.ensureLoaded(),
    studentsStore.fetchByExternalId(route.params.id),
    facultyStore.fetchList(),
  ])
  hydrateForm(detail)
}

onMounted(loadData)
watch(() => route.params.id, loadData)

const requiredRule = (v) => Boolean(v && String(v).trim()) || t('common.required')

async function handleSubmit() {
  const { valid } = await formRef.value.validate()
  if (!valid) return

  const resolvedAdmissionRoundId =
    childRoundItems.value.length > 0 ? form.childAdmissionRoundId : form.parentAdmissionRoundId

  submitting.value = true
  errorMessage.value = ''
  try {
    await studentsStore.updateStudent(route.params.id, {
      firstNameTh: form.firstNameTh.trim(),
      lastNameTh: form.lastNameTh.trim(),
      firstNameEn: form.firstNameEn.trim(),
      lastNameEn: form.lastNameEn.trim(),
      studyYear: Number(form.studyYear) || 1,
      majorExternalId: form.majorExternalId,
      departmentExternalId: form.departmentExternalId || null,
      admissionRoundExternalId: resolvedAdmissionRoundId || null,
      academicAdvisorExternalId: form.academicAdvisorExternalId || null,
      highSchoolTh: form.highSchoolTh.trim() || null,
      highSchoolEn: form.highSchoolEn.trim() || null,
    })

    await router.push(`/students/view/${route.params.id}`)
  } catch (err) {
    errorMessage.value = err.message || t('common.apiError')
  } finally {
    submitting.value = false
  }
}
</script>

<template>
  <v-progress-linear v-if="studentsStore.loading && !student" indeterminate color="secondary" class="mb-4" />

  <div v-if="student">
    <div class="cu-page-header">
      <div class="d-flex align-center ga-3">
        <v-btn icon="mdi-arrow-left" variant="outlined" :to="`/students/view/${student.externalId}`" />
        <div>
          <h1 class="text-h5 font-weight-bold">{{ t('student.editTitle') }}</h1>
          <div class="text-caption cu-muted">{{ student.studentCode }}</div>
        </div>
      </div>
    </div>

    <v-alert type="info" variant="tonal" density="compact" class="mb-5">
      {{ t('student.cuppepSyncBanner') }}
    </v-alert>

    <v-alert v-if="errorMessage" type="error" variant="tonal" class="mb-4">
      {{ errorMessage }}
    </v-alert>

    <v-form ref="formRef" @submit.prevent="handleSubmit">
      <v-card class="pa-6 mb-6">
        <v-row>
          <v-col cols="12" md="6">
            <v-text-field
              v-model="form.firstNameTh"
              :label="t('faculty.firstNameTh')"
              :rules="[requiredRule]"
            />
          </v-col>
          <v-col cols="12" md="6">
            <v-text-field
              v-model="form.lastNameTh"
              :label="t('faculty.lastNameTh')"
              :rules="[requiredRule]"
            />
          </v-col>
          <v-col cols="12" md="6">
            <v-text-field
              v-model="form.firstNameEn"
              :label="t('faculty.firstNameEn')"
              :rules="[requiredRule]"
            />
          </v-col>
          <v-col cols="12" md="6">
            <v-text-field
              v-model="form.lastNameEn"
              :label="t('faculty.lastNameEn')"
              :rules="[requiredRule]"
            />
          </v-col>

          <v-col cols="12" md="2">
            <v-text-field
              v-model.number="form.studyYear"
              :label="t('student.studyYear')"
              type="number"
              min="1"
              max="8"
            />
          </v-col>

          <v-col cols="12" md="5">
            <v-select
              v-model="form.majorExternalId"
              :items="majorItems"
              :label="t('common.major')"
              :rules="[requiredRule]"
            />
          </v-col>

          <v-col cols="12" md="5">
            <v-select
              v-model="form.departmentExternalId"
              :items="departmentItems"
              :label="t('student.departmentSelect')"
              :disabled="authStore.isStaff"
              clearable
            />
          </v-col>

          <v-col cols="12" md="4">
            <v-select
              v-model="form.academicAdvisorExternalId"
              :items="advisorItems"
              :label="t('student.advisorSelect')"
              clearable
            />
          </v-col>

          <v-col cols="12" md="4">
            <v-select
              v-model="form.parentAdmissionRoundId"
              :items="parentRoundItems"
              :label="t('student.admissionCategory')"
              clearable
              data-testid="student-admission-parent"
            />
          </v-col>

          <v-col cols="12" md="4">
            <v-select
              v-model="form.childAdmissionRoundId"
              :items="childRoundItems"
              :label="t('student.admissionSubRound')"
              :disabled="childRoundItems.length === 0"
              data-testid="student-admission-child"
            />
          </v-col>

          <v-col cols="12" md="6">
            <v-text-field
              v-model="form.highSchoolTh"
              :label="`${t('student.sec4HighSchool')} (TH)`"
            />
          </v-col>
          <v-col cols="12" md="6">
            <v-text-field
              v-model="form.highSchoolEn"
              :label="`${t('student.sec4HighSchool')} (EN)`"
            />
          </v-col>
        </v-row>
      </v-card>

      <div class="d-flex justify-end ga-3">
        <v-btn variant="outlined" :disabled="submitting" :to="`/students/view/${student.externalId}`">
          {{ t('common.cancel') }}
        </v-btn>
        <v-btn
          color="secondary"
          type="submit"
          :loading="submitting"
          prepend-icon="mdi-content-save-outline"
        >
          {{ t('common.save') }}
        </v-btn>
      </div>
    </v-form>
  </div>

  <v-alert v-else-if="!studentsStore.loading" type="warning" variant="tonal">
    {{ studentsStore.error || t('common.noData') }}
  </v-alert>
</template>
