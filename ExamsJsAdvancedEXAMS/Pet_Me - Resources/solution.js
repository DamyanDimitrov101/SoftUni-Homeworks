function solve() {
    let buttonElement = document.querySelector('#container button');
    let [name, age, kind, currentOwner] = Array.from(document.querySelectorAll('#container input'));


    buttonElement.addEventListener('click', addPet);

    function addPet(e) {
        e.preventDefault();

        let ulAdoptionElement = document.querySelector('#adoption > ul');

        if (Array.from(document.querySelectorAll('#container input')).some(a => !a.value)) {
            return;
        }

        if (!Number(age.value)) {
            return;
        }

        let newLiItem = document.createElement('li');
        newLiItem.innerHTML = `<p><strong>${name.value}</strong> is a <strong>${age.value}</strong> year old <strong>${kind.value}</strong></p> <span>Owner: ${currentOwner.value}</span>`;
        let contactButtonElement = document.createElement('button');
        contactButtonElement.textContent = 'Contact with owner';
        newLiItem.appendChild(contactButtonElement);
        ulAdoptionElement.appendChild(newLiItem);

        name.value = '';
        age.value = '';
        kind.value = '';
        currentOwner.value = '';

        contactButtonElement.addEventListener('click', onContact);

    }
    
    function onContact(e) {
        let parentLiElement = e.currentTarget.parentElement;
        e.currentTarget.remove();
        let divToContactElement = document.createElement('div');
        
        divToContactElement.innerHTML = `<input placeholder="Enter your names" /> <button>Yes! I take it!</button>`;
        parentLiElement.appendChild(divToContactElement);
        
        let buttonItakeItElement = divToContactElement.querySelector('button');
        
        buttonItakeItElement.addEventListener('click',onItakeIt);
    }
    
    
    function onItakeIt(e) {
        let liElement = e.currentTarget.parentElement.parentElement;
        let inputNameElement = e.currentTarget.previousElementSibling;
        let ulAdoptedElement = document.querySelector('#adopted > ul');
        let spanElementWithOwnerInfo = liElement.querySelector('span');
        
        if (!inputNameElement.value) {
            return;
        }
        
        ulAdoptedElement.appendChild(liElement);
        
        spanElementWithOwnerInfo.textContent = `New Owner: ${inputNameElement.value}`;
        e.currentTarget.parentElement.remove();
        let checkedButtonElement = document.createElement('button');
        checkedButtonElement.textContent=  'Checked';
        liElement.appendChild(checkedButtonElement);
        
        checkedButtonElement.addEventListener('click',onChecked);

    }
    
    function onChecked(e) {
        let liElement = e.currentTarget.parentElement;
        liElement.remove();
    }
}
