function create(words) {
   let contentDivElement = document.getElementById('content');
   
   for (const word of words) {
      let div = document.createElement('div');
      let p = document.createElement('p');
      p.textContent= word;
      p.style.display = 'none';
      div.appendChild(p);
      contentDivElement.appendChild(div);
   }

   contentDivElement.addEventListener('click',onClick);

   function onClick(e) {
      let targetP = e.target.children[0];

      e.target.children[0].style.display = 'block';
   }
}