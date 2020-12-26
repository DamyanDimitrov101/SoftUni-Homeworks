function notify(message) {
    let divElement = document.getElementById('notification');
    
    divElement.textContent = message;
    
    divElement.style.display = 'block';
    
    setTimeout(()=>{
        divElement.style.display = 'none';
    },2000);

}