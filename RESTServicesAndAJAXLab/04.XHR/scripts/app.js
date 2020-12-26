function loadRepos() {
   let resElement = document.getElementById('res');

   let url = `https://api.github.com/repos/testnakov/test-nakov-repo`;

   let httpRequest = new XMLHttpRequest();

   httpRequest.addEventListener('loadend',function () {
      let responseText = this.responseText;


      resElement.innerHTML = responseText;
   })

   httpRequest.open('GET',url);
   httpRequest.send();
}