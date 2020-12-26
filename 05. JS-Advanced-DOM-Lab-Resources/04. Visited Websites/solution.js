function solve() {
   let aElements = document.querySelectorAll('.link-1 a');

   for (const linkElement of aElements) {
     linkElement.addEventListener('click',onLinkElementClick);

     function onLinkElementClick(e) {
          let paragraphElement = e.currentTarget.nextElementSibling;

          let count = (paragraphElement.innerHTML.split(' ')[1]);
          count++;
          
          paragraphElement.innerHTML = `visited ${count} times`;
     }
   }
}