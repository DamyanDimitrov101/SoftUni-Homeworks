function growingWord() {

    let headingElement = document.getElementById('exercise').getElementsByTagName('p')[0];
    
    console.log(headingElement);


    if (!headingElement.style.fontSize) {
      headingElement.style.fontSize = '2px';
      headingElement.style.color = 'blue';
    }else{
      headingElement.style.fontSize = (parseInt(headingElement.style.fontSize)*2)+'px';
      
      if (headingElement.style.color=='blue') {
        headingElement.style.color = 'green';
      }
      else if (headingElement.style.color=='green') {
        headingElement.style.color = 'red';
      }
      else if(headingElement.style.color=='red'){
        headingElement.style.color = 'blue';
      }
      
    }
}