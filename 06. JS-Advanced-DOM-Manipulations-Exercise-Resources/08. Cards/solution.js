function solve() {
   let section = document.getElementsByClassName('cards')[0];
   section.addEventListener('click',onClick);



   function onClick(e) {
      let cardClicked = e.target;
      if (cardClicked.tagName === 'IMG') {
         let first = document.querySelectorAll('#result span')[0];
         let second = document.querySelectorAll('#result span')[2];
         
         cardClicked.src= 'images/whiteCard.jpg';
         
         if (cardClicked.parentElement.id==='player1Div') {
            first.textContent = cardClicked.name;
         }else{
            second.textContent = cardClicked.name;
         }
         
         if (first.textContent!=='' && second.textContent!=='') {
               if (Number(first.textContent)>Number(second.textContent)) {
                  let winner = [...document.querySelectorAll('.cards img')]
                              .filter(x=>Number(x.name)===Number(first.textContent))[0];
                  winner.style.border = '2px solid green';
                  let looser = [...document.querySelectorAll('.cards img')]
                  .filter(x=>Number(x.name)===Number(second.textContent))[0];
                  looser.style.border = '2px solid red';
               }else{
                  let winner = [...document.querySelectorAll('.cards img')]
                              .filter(x=>Number(x.name)===Number(second.textContent))[0];
                  winner.style.border = '2px solid green';
                  let looser = [...document.querySelectorAll('.cards img')]
                  .filter(x=>Number(x.name)===Number(first.textContent))[0];
                  looser.style.border = '2px solid red';
               }

               
               let historyEl = Array.from(document.querySelectorAll('div')).filter(x=>x.id ==='history')[0];
               
               historyEl.textContent += `[${first.textContent} vs ${second.textContent}]`;
               
               
               first.textContent = '';
               second.textContent = '';
         }
      }
   }
}