<script setup>
import { computed, onMounted, reactive, ref, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { useAuthStore } from '@/stores/auth'
import { useReferenceDataStore } from '@/stores/referenceData'
import { useLocalizedField } from '@/composables/useLocalizedField'

const props = defineProps({
  initialValue: {
    type: Object,
    default: null,
  },
  isEdit: {
    type: Boolean,
    default: false,
  },
  submitting: {
    type: Boolean,
    default: false,
  },
})

const emit = defineEmits(['submit', 'cancel'])

const { t } = useI18n()
const authStore = useAuthStore()
const refStore = useReferenceDataStore()
const { locale } = useLocalizedField()

const formRef = ref(null)

const form = reactive({
  personnelCode: '',
  professionalTitleTh: 'ดร.ภก.',
  professionalTitleEn: 'Dr. Pharm.',
  firstNameTh: '',
  lastNameTh: '',
  firstNameEn: '',
  lastNameEn: '',
  academicPositionExternalId: null,
  administrativePositionsTh: '',
  administrativePositionsEn: '',
  departmentExternalId: null,
  email: '',
  phoneNumber: '',
  scopusAuthorId: '',
  orcidId: '',
  educations: [
    {
      degreeTh: '',
      degreeEn: '',
      institutionTh: '',
      institutionEn: '',
      graduationYear: new Date().getFullYear(),
    },
  ],
  publications: [],
})

const isDepartmentLocked = computed(
  () => authStore.isStaff && Boolean(authStore.resolvedDepartmentExternalId),
)

const departmentItems = computed(() =>
  refStore.departments.map((d) => ({
    value: d.externalId,
    title: locale.value === 'en' ? d.nameEn : d.nameTh,
  })),
)

const positionItems = computed(() =>
  refStore.academicPositions.map((p) => ({
    value: p.externalId,
    title: locale.value === 'en' ? p.titleEn : p.titleTh,
  })),
)

function hydrateForm(value) {
  if (!value) return
  form.personnelCode = value.personnelCode || ''
  form.professionalTitleTh = value.professionalTitleTh || ''
  form.professionalTitleEn = value.professionalTitleEn || ''
  form.firstNameTh = value.firstNameTh || ''
  form.lastNameTh = value.lastNameTh || ''
  form.firstNameEn = value.firstNameEn || ''
  form.lastNameEn = value.lastNameEn || ''
  form.academicPositionExternalId = value.academicPositionExternalId || null
  form.administrativePositionsTh = value.administrativePositionsTh || ''
  form.administrativePositionsEn = value.administrativePositionsEn || ''
  form.departmentExternalId = value.departmentExternalId || null
  form.email = value.email || ''
  form.phoneNumber = value.phoneNumber || ''
  form.scopusAuthorId = value.scopusAuthorId || ''
  form.orcidId = value.orcidId || ''
  form.educations = value.educations?.length
    ? value.educations.map((e) => ({
        degreeTh: e.degreeTh || '',
        degreeEn: e.degreeEn || '',
        institutionTh: e.institutionTh || '',
        institutionEn: e.institutionEn || '',
        graduationYear: e.graduationYear || new Date().getFullYear(),
      }))
    : [
        {
          degreeTh: '',
          degreeEn: '',
          institutionTh: '',
          institutionEn: '',
          graduationYear: new Date().getFullYear(),
        },
      ]
  form.publications = value.publications?.length
    ? value.publications.map((p) => ({
        title: p.title || '',
        journalOrVenue: p.journalOrVenue || '',
        publishedYear: p.publishedYear || new Date().getFullYear(),
        doi: p.doi || '',
        scopusId: p.scopusId || '',
      }))
    : []
}

function applyDefaultsForCreate() {
  if (props.isEdit || props.initialValue) return
  if (!form.academicPositionExternalId && refStore.academicPositions.length > 0) {
    form.academicPositionExternalId = refStore.academicPositions[0].externalId
  }
  if (!form.departmentExternalId) {
    form.departmentExternalId =
      authStore.resolvedDepartmentExternalId || refStore.departments[0]?.externalId || null
  }
}

watch(
  () => props.initialValue,
  (val) => {
    if (val) hydrateForm(val)
  },
  { immediate: true },
)

onMounted(async () => {
  await refStore.ensureLoaded()
  applyDefaultsForCreate()
})

const requiredRule = (v) => Boolean(v && String(v).trim()) || t('common.required')

function addEducationRow() {
  form.educations.push({
    degreeTh: '',
    degreeEn: '',
    institutionTh: '',
    institutionEn: '',
    graduationYear: new Date().getFullYear(),
  })
}

function removeEducationRow(index) {
  if (form.educations.length > 1) {
    form.educations.splice(index, 1)
  }
}

function addPublicationRow() {
  form.publications.push({
    title: '',
    journalOrVenue: '',
    publishedYear: new Date().getFullYear(),
    doi: '',
    scopusId: '',
  })
}

function removePublicationRow(index) {
  form.publications.splice(index, 1)
}

function toNullable(val) {
  const trimmed = String(val || '').trim()
  return trimmed || null
}

async function handleSubmit() {
  const { valid } = await formRef.value.validate()
  if (!valid) return

  const payload = {
    personnelCode: form.personnelCode.trim(),
    professionalTitleTh: toNullable(form.professionalTitleTh),
    professionalTitleEn: toNullable(form.professionalTitleEn),
    firstNameTh: form.firstNameTh.trim(),
    lastNameTh: form.lastNameTh.trim(),
    firstNameEn: form.firstNameEn.trim(),
    lastNameEn: form.lastNameEn.trim(),
    academicPositionExternalId: form.academicPositionExternalId,
    administrativePositionsTh: toNullable(form.administrativePositionsTh),
    administrativePositionsEn: toNullable(form.administrativePositionsEn),
    departmentExternalId: form.departmentExternalId,
    email: form.email.trim(),
    phoneNumber: toNullable(form.phoneNumber),
    scopusAuthorId: toNullable(form.scopusAuthorId),
    orcidId: toNullable(form.orcidId),
    educations: form.educations.map((edu, idx) => ({
      degreeTh: edu.degreeTh.trim(),
      degreeEn: toNullable(edu.degreeEn),
      institutionTh: edu.institutionTh.trim(),
      institutionEn: toNullable(edu.institutionEn),
      graduationYear: Number(edu.graduationYear) || new Date().getFullYear(),
      sortOrder: idx + 1,
    })),
    publications: form.publications.map((pub) => ({
      title: pub.title.trim(),
      journalOrVenue: toNullable(pub.journalOrVenue),
      publishedYear: Number(pub.publishedYear) || new Date().getFullYear(),
      doi: toNullable(pub.doi),
      scopusId: toNullable(pub.scopusId),
    })),
  }

  emit('submit', payload)
}
</script>

<template>
  <v-form ref="formRef" @submit.prevent="handleSubmit">
    <!-- Section 1–5: Core Bilingual Profile -->
    <v-card class="pa-6 mb-5">
      <div class="cu-section-title mb-4">
        <v-icon icon="mdi-account-card-outline" color="primary" />
        {{ t('faculty.sec1Name') }} &amp; {{ t('faculty.sec2Position') }}
      </div>

      <v-row>
        <v-col cols="12" md="4">
          <v-text-field
            v-model="form.personnelCode"
            :label="t('faculty.personnelCode')"
            :rules="[requiredRule]"
            :disabled="isEdit"
            placeholder="PHARM-1004"
            data-testid="faculty-form-code"
          />
        </v-col>
        <v-col cols="12" md="4">
          <v-select
            v-model="form.academicPositionExternalId"
            :items="positionItems"
            :label="t('faculty.academicPosition')"
            :rules="[requiredRule]"
            :loading="refStore.loading"
          />
        </v-col>
        <v-col cols="12" md="4">
          <v-select
            v-model="form.departmentExternalId"
            :items="departmentItems"
            :label="t('common.department')"
            :rules="[requiredRule]"
            :disabled="isDepartmentLocked"
            :loading="refStore.loading"
          />
        </v-col>

        <v-col cols="12" md="2">
          <v-text-field
            v-model="form.professionalTitleTh"
            :label="`${t('faculty.professionalTitle')} (TH)`"
          />
        </v-col>
        <v-col cols="12" md="5">
          <v-text-field
            v-model="form.firstNameTh"
            :label="t('faculty.firstNameTh')"
            :rules="[requiredRule]"
            data-testid="faculty-form-firstname-th"
          />
        </v-col>
        <v-col cols="12" md="5">
          <v-text-field
            v-model="form.lastNameTh"
            :label="t('faculty.lastNameTh')"
            :rules="[requiredRule]"
            data-testid="faculty-form-lastname-th"
          />
        </v-col>

        <v-col cols="12" md="2">
          <v-text-field
            v-model="form.professionalTitleEn"
            :label="`${t('faculty.professionalTitle')} (EN)`"
          />
        </v-col>
        <v-col cols="12" md="5">
          <v-text-field
            v-model="form.firstNameEn"
            :label="t('faculty.firstNameEn')"
            :rules="[requiredRule]"
          />
        </v-col>
        <v-col cols="12" md="5">
          <v-text-field
            v-model="form.lastNameEn"
            :label="t('faculty.lastNameEn')"
            :rules="[requiredRule]"
          />
        </v-col>

        <v-col cols="12" md="6">
          <v-text-field
            v-model="form.administrativePositionsTh"
            :label="`${t('faculty.adminPositions')} (TH)`"
          />
        </v-col>
        <v-col cols="12" md="6">
          <v-text-field
            v-model="form.administrativePositionsEn"
            :label="`${t('faculty.adminPositions')} (EN)`"
          />
        </v-col>

        <v-col cols="12" md="6">
          <v-text-field
            v-model="form.email"
            :label="t('faculty.email')"
            type="email"
            :rules="[requiredRule]"
            placeholder="name@pharm.chula.ac.th"
          />
        </v-col>
        <v-col cols="12" md="6">
          <v-text-field v-model="form.phoneNumber" :label="t('faculty.phone')" />
        </v-col>

        <v-col cols="12" md="6">
          <v-text-field
            v-model="form.scopusAuthorId"
            :label="t('faculty.scopusAuthorId')"
            prepend-inner-icon="mdi-book-search-outline"
            placeholder="57191234567"
          />
        </v-col>
        <v-col cols="12" md="6">
          <v-text-field
            v-model="form.orcidId"
            :label="t('faculty.orcidId')"
            prepend-inner-icon="mdi-identifier"
            placeholder="0000-0002-1825-0097"
          />
        </v-col>
      </v-row>
    </v-card>

    <!-- Section 6: Education History -->
    <v-card class="pa-6 mb-5">
      <div class="d-flex align-center justify-space-between mb-4">
        <div class="cu-section-title">
          <v-icon icon="mdi-school-outline" color="primary" />
          {{ t('faculty.sec6Education') }}
        </div>
        <v-btn size="small" color="secondary" variant="tonal" prepend-icon="mdi-plus" @click="addEducationRow">
          {{ t('faculty.addEducation') }}
        </v-btn>
      </div>

      <v-row
        v-for="(edu, idx) in form.educations"
        :key="idx"
        class="pa-3 mb-3 rounded-lg bg-surface-variant align-center"
      >
        <v-col cols="12" md="3">
          <v-text-field v-model="edu.degreeTh" :label="`${t('faculty.degree')} (TH)`" :rules="[requiredRule]" />
        </v-col>
        <v-col cols="12" md="2">
          <v-text-field v-model="edu.degreeEn" :label="`${t('faculty.degree')} (EN)`" />
        </v-col>
        <v-col cols="12" md="2">
          <v-text-field v-model="edu.institutionTh" :label="`${t('faculty.institution')} (TH)`" :rules="[requiredRule]" />
        </v-col>
        <v-col cols="12" md="2">
          <v-text-field v-model="edu.institutionEn" :label="`${t('faculty.institution')} (EN)`" />
        </v-col>
        <v-col cols="12" md="2">
          <v-text-field v-model.number="edu.graduationYear" :label="t('faculty.gradYear')" type="number" />
        </v-col>
        <v-col cols="12" md="1" class="text-right">
          <v-btn
            icon="mdi-delete-outline"
            variant="text"
            color="error"
            size="small"
            :disabled="form.educations.length === 1"
            @click="removeEducationRow(idx)"
          />
        </v-col>
      </v-row>
    </v-card>

    <!-- Section 7: Publications -->
    <v-card class="pa-6 mb-6">
      <div class="d-flex align-center justify-space-between mb-4">
        <div class="cu-section-title">
          <v-icon icon="mdi-file-document-multiple-outline" color="primary" />
          {{ t('faculty.sec7Publications') }}
        </div>
        <v-btn size="small" color="secondary" variant="tonal" prepend-icon="mdi-plus" @click="addPublicationRow">
          {{ t('faculty.addPublication') }}
        </v-btn>
      </div>

      <v-alert v-if="form.publications.length === 0" type="info" variant="tonal" density="compact">
        {{ t('common.none') }}
      </v-alert>

      <v-row
        v-for="(pub, idx) in form.publications"
        :key="idx"
        class="pa-3 mb-3 rounded-lg bg-surface-variant align-center"
      >
        <v-col cols="12" md="4">
          <v-text-field v-model="pub.title" :label="t('faculty.pubTitle')" :rules="[requiredRule]" />
        </v-col>
        <v-col cols="12" md="3">
          <v-text-field v-model="pub.journalOrVenue" :label="t('faculty.journal')" />
        </v-col>
        <v-col cols="12" md="1">
          <v-text-field v-model.number="pub.publishedYear" :label="t('faculty.pubYear')" type="number" />
        </v-col>
        <v-col cols="12" md="2">
          <v-text-field v-model="pub.doi" :label="t('faculty.doi')" />
        </v-col>
        <v-col cols="12" md="1">
          <v-text-field v-model="pub.scopusId" :label="t('faculty.scopusId')" />
        </v-col>
        <v-col cols="12" md="1" class="text-right">
          <v-btn icon="mdi-delete-outline" variant="text" color="error" size="small" @click="removePublicationRow(idx)" />
        </v-col>
      </v-row>
    </v-card>

    <div class="d-flex justify-end ga-3">
      <v-btn variant="outlined" :disabled="submitting" @click="emit('cancel')">
        {{ t('common.cancel') }}
      </v-btn>
      <v-btn
        color="primary"
        type="submit"
        :loading="submitting"
        prepend-icon="mdi-content-save-outline"
        data-testid="faculty-form-submit"
      >
        {{ t('common.save') }}
      </v-btn>
    </div>
  </v-form>
</template>
