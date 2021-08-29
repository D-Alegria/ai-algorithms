using UnityEngine;

namespace Core
{
    public abstract class SessionMember : MonoBehaviour
    {
        readonly ISession _session = Session.Instance;

        protected void OnEnable()
        {
            _session.ONStateChanged += ONSessionStateChanged;
            //Debug.Log("subscribed");
        }

        // Update is called once per frame
        protected void OnDisable()
        {
            _session.ONStateChanged -= ONSessionStateChanged;
            //Debug.Log("unsubscribed");
        }

        protected abstract void ONSessionStateChanged(SessionState state);
    }
}

