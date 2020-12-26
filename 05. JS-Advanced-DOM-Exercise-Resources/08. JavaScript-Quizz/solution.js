function solve() {
  let correctAnswers = ["onclick", "JSON.stringify()", "A programming API for HTML and XML documents"];
  let points = 0;
  let index = 0;


  Array
    .from(document.querySelectorAll('.answer-text'))
    .map(x=>x.addEventListener('click', function nextSection(e){
        if (correctAnswers.includes(e.target.innerText)) {
          points++;
        }

        let currentSection = document.querySelectorAll('section')[index];
        currentSection.style.display = 'none';

        let nextSection = document.querySelectorAll('section')[index+1];
        if (nextSection!==undefined) {
            nextSection.style.display = 'block';
            index++;
        }else{
          document.querySelector('#results').style.display = 'block';
          let headingElement = document.querySelector('#results h1');
          if (points===3) {
              headingElement.textContent = "You are recognized as top JavaScript fan!";
          }else{
            headingElement.textContent = `You have ${points} right answers`;
          }
        }
    }))
}