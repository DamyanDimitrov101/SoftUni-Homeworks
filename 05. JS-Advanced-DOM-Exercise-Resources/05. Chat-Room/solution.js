function solve() {
   let buttonElement = document.getElementById('send');

   let chatDivElements = document.getElementById('chat_messages');

   let inputElement = document.getElementById('chat_input');

   buttonElement.addEventListener('click',sendMessage);

   let turn = 'me';

   function sendMessage() {
      let message = inputElement.value;

      if (message!='') {
         
      

      let divMessageToCreate = document.createElement('div');
      divMessageToCreate.classList.add('message');
      divMessageToCreate.classList.add('my-message');
      divMessageToCreate.innerHTML = message;
      
      
      // let divProfileToCreate = document.createElement('div');
      // let attP = document.createAttribute('class');
      // attP.value = turn==='me'?'profile my-profile':'profile other-profile';
      // divProfileToCreate.setAttributeNode(attP);
      // divProfileToCreate.innerHTML = turn==='me'?'<span>My profile</span> <img src="https://images.unsplash.com/photo-1534135954997-e58fbd6dbbfc?ixlib=rb-0.3.5&q=80&fm=jpg&crop=entropy&cs=tinysrgb&w=400&fit=max&ixid=eyJhcHBfaWQiOjE0NTg5fQ&s=02d536c38d9cfeb4f35f17fdfaa36619" width="30" height="30" />':'<img src="https://images.unsplash.com/photo-1537396123722-b93d0acd8848?ixlib=rb-0.3.5&q=80&fm=jpg&crop=entropy&cs=tinysrgb&w=400&fit=max&ixid=eyJhcHBfaWQiOjE0NTg5fQ&s=efc6e85c24d3cfdd15cd36cb8a2471ed" width="30" height="30" /> <span>Other profile</span>';


      //chatDivElements.insertBefore(divMessageToCreate,chatDivElements.firstChild);
      // chatDivElements.insertBefore(divProfileToCreate,chatDivElements.firstChild);


      chatDivElements.appendChild(divMessageToCreate);

      inputElement.value = '';

      // if (turn=='me') {
      //    turn = 'other';
      // }else{
      //    turn = 'me';
      // }
     }
   }

}


