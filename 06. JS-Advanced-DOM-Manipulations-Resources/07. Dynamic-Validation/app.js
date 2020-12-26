function validate() {
    let inputElement = document.getElementById('email');

    inputElement.addEventListener('change', onChange);


    function onChange(e) {
        let emailText = e.target.value;
        if (ValidateEmail(emailText)) {
            e.target.removeAttribute('class');
            return;
        } 
        
        e.target.className = 'error';
    }

    function ValidateEmail(emailText) {
        var mailformat = /^[a-z]+@[a-z]+(?:\.[a-z]+)*$/;
        if (emailText.match(mailformat)) {
            inputElement.focus();
            return true;
        }
        else {
            inputElement.focus();
            return false;
        }
    }
}