using UnityEngine;

namespace ScrambleKit
{
    public class HitPoints : MonoBehaviour
    {
        public int maxHP = 3;
        public int hp;

        public FXTrigger HitFX;
		public Transform CorpsePrefab;

		void Start()
        {
            hp = maxHP;
        }

        public void TakeDamage(int dmg)
        {
            if (hp <= 0)
                return;

			HitFX.Trigger(transform);

            hp = Mathf.Max(0, hp - dmg);
            if (hp <= 0)
                Die();
        }

        private void Die()
		{
			transform.root.SendMessage("Dying", SendMessageOptions.DontRequireReceiver);
			DropCorpse();
			Destroy(gameObject);
		}

		private void DropCorpse()
		{
			if (CorpsePrefab == null)
				return;

			Transform corpse = Instantiate(CorpsePrefab);
			corpse.position = transform.root.position;
		}
	}
}
