using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;
public class InputHandler : MonoBehaviour
{
    public Camera _mainCamera;
    private bool pausa = false;
    private void Awake()
    {
        
        
    }

    public void OnClick(InputAction.CallbackContext context)
    {

        if (!pausa){
            if (!context.started) return;

            var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));

            if (!rayHit.collider) return;

            Debug.Log(rayHit.collider.name + " click derecho");
            GameObject objetoseleccionado = rayHit.collider.gameObject;
            ObjetosTienda Obj = objetoseleccionado.GetComponent<ObjetosTienda>();
            GestorDeObjetosEnTienda gestor = objetoseleccionado.GetComponent<GestorDeObjetosEnTienda>();
            Vector3 pos = gestor.nuevaPosicion(Obj);
            Obj.jugarobj(pos);
            
            
        }
        
    }

    public void ClickDerecho(InputAction.CallbackContext context)
    {
        if(!pausa){
            if (!context.started) return;

            var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));

            if (!rayHit.collider) return;

            Debug.Log(rayHit.collider.name + " click derecho");
            GameObject objetoseleccionado = rayHit.collider.gameObject;
            ObjetosTienda Obj = objetoseleccionado.GetComponent<ObjetosTienda>();
            Obj.zoom();
        }
    }
    public void Tactil(InputAction.CallbackContext context)
    {

        if (!pausa){
            
            if (!context.started) return;
            

            var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Touchscreen.current.primaryTouch.position.ReadValue()));

            if (!rayHit.collider) return;

            Debug.Log(rayHit.collider.name + " click derecho");
            GameObject objetoseleccionado = rayHit.collider.gameObject;
            ObjetosTienda Obj = objetoseleccionado.GetComponent<ObjetosTienda>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
