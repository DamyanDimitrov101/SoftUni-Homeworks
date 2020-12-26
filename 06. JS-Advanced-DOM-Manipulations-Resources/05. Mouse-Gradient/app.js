function attachGradientEvents() {
    let divButtonElement = document.getElementById('gradient');
    let resultElement= document.getElementById('result');
    
    divButtonElement.addEventListener('mousemove',onMoveButton);
    divButtonElement.addEventListener('mouseout',onOutButton);

    function onMoveButton(e) {
        let buttonWidth = e.target.clientWidth-1;
        let offset = e.offsetX;
        let percent = (offset/buttonWidth)*100;
        console.log(e);
        resultElement.textContent = `${Math.floor(percent)}%`;
    }

    function onOutButton(e) {
        resultElement.textContent = '';
    }
}