function deleteByEmail() {
    let input = document.getElementsByName('email')[0].value.trim();
   
    let trElements = document.getElementsByTagName('tr');
    let emailExist = false;
    let resultsElement = document.getElementById('result');
    
    for (const tr of trElements) {
        let currentEmail = tr.children[1].textContent;
        if (input===currentEmail) {
            tr.remove();
            emailExist = true;
            resultsElement.innerHTML= 'Deleted.';
        }
    }
    if (!emailExist) {
        resultsElement.innerHTML+= 'Not found.';
    }
}