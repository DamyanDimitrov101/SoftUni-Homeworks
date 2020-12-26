function addItem() {
    let ulElement = document.getElementById('items');
    let inputElement = document.getElementById('newText');

    let liElement = document.createElement('li');
    let aElementDelete = document.createElement('a');
    aElementDelete.textContent= '[Delete]';
    aElementDelete.href = '#';
    aElementDelete.addEventListener('click',deleteElement);
    aElementDelete.addEventListener('mouseover',function () {
        aElementDelete.style.cursor = 'pointer';
    })

    liElement.innerHTML = inputElement.value+'    ';
    liElement.appendChild(aElementDelete);

    ulElement.appendChild(liElement);
    inputElement.value = '';

    function deleteElement(e) {
        let liElementToDel = e.target.parentElement;

        liElementToDel.remove();
    }
}