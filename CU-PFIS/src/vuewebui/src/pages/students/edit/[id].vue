<script setup>
import { computed, reactive, ref, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { useRoute, useRouter } from 'vue-router'
import { useStudentsStore } from '@/stores/students'
import { useReferenceDataStore } from '@/stores/referenceData'
import { useLocalizedField } from '@/composables/useLocalizedField'

const { t } = useI18n()
const route = useRoute()
const router = useRouter()
const studentsStore = useStudentsStore()
const refStore = useReferenceDataStore()
const { locale } = useLocalizedField()

const student = computed(() => studentsStore.getByExternalId(route.params.id))

const formRef = ref(null)

function resolveInitialParentRound(roundId) {
  const found = refStore.admissionRounds.find((r) => r.externalId === roundId)
  if (!found) return null
  return found.parentExternalId || found.externalId
}

function resolveInitialChildRound(roundId) {
  const found = refStore.admissionRounds.find((r) => r.externalId === roundId)
  if (!found || !found.parentExternalId) return null
  return found.externalId
}

const form = reactive({
  firstNameTh: student.value?.firstNameTh || '',
  lastNameTh: student.value?.lastNameTh || '',
  firstNameEn: student.value?.firstNameEn || '',
  lastNameEn: student.value?.lastNameEn || '',
  majorExternalId: student.value?.majorExternalId || 'major-pc',
  parentAdmissionRoundId: resolveInitialParentRound(student.value?.admissionRoundExternalId),
  childAdmissionRoundId: resolveInitialChildRound(student.value?.admissionRoundExternalId),
  highSchoolTh: student.value?.highSchoolTh || '',
  highSchoolEn: student.value?.highSchoolEn || '',
})

const majorItems = computed(() =>
  refStore.majors.map((m) => ({
    value: m.externalId,
    title: locale.value === 'en' ? m.nameEn : m.nameTh,
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

watch(
  () => form.parentAdmissionRoundId,
  () => {
    const validChildren = childRoundItems.value.map((c) => c.value)
    if (!validChildren.includes(form.childAdmissionRoundId)) {
      form.childAdmissionRoundId = validChildren[0] || null
    }
  },
)

const requiredRule = (v) => Boolean(v && String(v).trim()) || t('common.required')

async function handleSubmit() {
  const { valid } = await formRef.value.validate()
  if (!valid) return

  const resolvedAdmissionRoundId =
    childRoundItems.value.length > 0 ? form.childAdmissionRoundId : form.parentAdmissionRoundId

  studentsStore.updateStudent(route.params.id, {
    firstNameTh: form.firstNameTh,
    lastNameTh: form.lastNameTh,
    firstNameEn: form.firstNameEn,
    lastNameEn: form.lastNameEn,
    majorExternalId: form.majorExternalId,
    admissionRoundExternalId: resolvedAdmissionRoundId,
    highSchoolTh: form.highSchoolTh,
    highSchoolEn: form.highSchoolEn,
  })

  router.push(`/students/view/${route.params.id}`)
}
</script>

<template>
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

          <v-col cols="12" md="4">
            <v-select
              v-model="form.majorExternalId"
              :items="majorItems"
              :label="t('common.major')"
              :rules="[requiredRule]"
            />
          </v-col>

          <v-col cols="12" md="4">
            <v-select
              v-model="form.parentAdmissionRoundId"
              :items="parentRoundItems"
              :label="t('student.admissionCategory')"
              :rules="[requiredRule]"
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
        <v-btn variant="outlined" :to="`/students/view/${student.externalId}`">
          {{ t('common.cancel') }}
        </v-btn>
        <v-btn color="secondary" type="submit" prepend-icon="mdi-content-save-outline">
          {{ t('common.save') }}
        </v-btn>
      </div>
    </v-form>
  </div>

  <v-alert v-else type="warning" variant="tonal">
    {{ t('common.noData') }}
  </v-alert>
</template>
