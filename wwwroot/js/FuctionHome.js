window.copiarContenido = (texto) => {

    if (texto === "") {
        alert("Elemento no copiado")
    } else {
        alert("Elemento copiado")
    }
    navigator.clipboard.writeText(texto);
};