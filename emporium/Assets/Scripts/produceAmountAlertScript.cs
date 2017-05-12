using UnityEngine;

public class produceAmountAlertScript : MonoBehaviour
{
    private float timeAlive = 1f;
    private Color color1;

    private Quaternion _lookRotation;
    private Vector3 _direction;

    // Update is called once per frame
    private void Update()
    {
        //TODO make color transition based on fruit color?? idk idea
        if (timeAlive > 0)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, new Vector3(transform.position.x, 2f, transform.position.z), Time.deltaTime);
            timeAlive -= Time.deltaTime;

            //  transform.LookAt(Camera.main.transform);

            //find the vector pointing from our position to the target
            _direction = (Camera.main.transform.position - transform.position).normalized;

            //create the rotation we need to be in to look at the target
            _lookRotation = Quaternion.LookRotation(-_direction);

            //rotate us over time according to speed until we are in the required rotation

            Quaternion newRot = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * 50);
            transform.rotation = new Quaternion(0, newRot.y, 0, newRot.w);

            gameObject.GetComponent<TextMesh>().color = Color.Lerp(Color.grey, Color.black, Mathf.PingPong(Time.time * 2, 1));
        }
        else
        {
            Destroy(gameObject);
        }
    }
}