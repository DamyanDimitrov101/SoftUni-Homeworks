function solve() {
    let inputPElement = document.getElementById('input');

    let arr = inputPElement.innerHTML.split('.').filter(x=>x!=='');
    
    let outputDivElement = document.getElementById('output');

    for (let i = 0; i < arr.length; i+=3) {
      let text = arr[i]+'. '+(arr[i+1]!==undefined?arr[i+1]+'. ':'')+(arr[i+2]!==undefined?arr[i+2]+'.':'');

      let paragraphElement = document.createElement('p');
      paragraphElement.innerHTML = text;

      outputDivElement.appendChild(paragraphElement);
    }

}


function GrowingWord() {
  
}