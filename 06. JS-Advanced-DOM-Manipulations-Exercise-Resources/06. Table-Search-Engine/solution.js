function solve() {
   let buttonEl = document.getElementById('searchBtn');
   let trElements = document.querySelectorAll('tbody > tr');

   buttonEl.addEventListener('click',onSearch);

   function onSearch(e) {
      let searchText = e.target.previousSibling.previousSibling.value.toLowerCase();
      let trToHihlight = Array.from(trElements).filter(x=>x.textContent.toLowerCase().includes(searchText));

      trToHihlight.forEach(x=> x.classList.add('select'))

      //console.log(trToHihlight[0].textContent);
   }
}