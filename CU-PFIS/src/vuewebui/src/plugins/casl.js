import { AbilityBuilder, createMongoAbility } from '@casl/ability'

export const ability = createMongoAbility([])

export function updateAbilityForPermissions(permissions = []) {
  const { can, rules } = new AbilityBuilder(createMongoAbility)

  for (const permission of permissions) {
    const [subject, action] = permission.split('.')
    if (subject && action) {
      can(action, subject)
    }
  }

  ability.update(rules)
}
