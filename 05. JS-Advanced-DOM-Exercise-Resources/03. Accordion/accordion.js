function toggle() {
    let moreButtonElement = document.getElementsByClassName('button')[0];

    let extraDivElement = document.getElementById('extra');

     if (extraDivElement.style.display === "none") {
        extraDivElement.style.display = "block";
        moreButtonElement.innerHTML = 'Less';    
     }else{
        extraDivElement.style.display = "none";
        moreButtonElement.innerHTML = "More";
     }    
}