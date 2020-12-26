function solve() {
    let buttonElement = document.querySelector('button');
    buttonElement.addEventListener('click',addPersonToList);
    

    let listItemsElements = document.getElementsByTagName('li');

    let alphabet = {};
    let index = 0;
    for (let i = 65; i <= 90; i++) {
        let letter = String.fromCharCode(i).toUpperCase();

        if (!alphabet[letter]) {
            alphabet[letter] = index++;
        }
    }

    function addPersonToList() {
        let nameStudentInputElement = document.querySelector('input');
        

        let name = (nameStudentInputElement.value.toString()[0].toUpperCase())+(nameStudentInputElement.value.slice(1).toLowerCase());
        let indexToAppend = alphabet[name[0]];
        let liElementToPut = listItemsElements[indexToAppend];

        if (liElementToPut.innerText!='') {
            liElementToPut.textContent+=', ';
        }

        liElementToPut.textContent+=name;
        nameStudentInputElement.value = '';
    }
}


