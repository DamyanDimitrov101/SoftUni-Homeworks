function encodeAndDecodeMessages() {
    let [encodeBtn,decodeBtn] = document.getElementsByTagName('button');
    let [encodeArea,decodeArea] = document.getElementsByTagName('textarea');

    let message = '';

    encodeBtn.addEventListener('click',encode);
    decodeBtn.addEventListener('click',decode);

    function encode(e) {
        message = encodeArea.value;
        let current = '';
        for (let index = 0; index < message.length; index++) {
            let asciiCode = message.charCodeAt(index)+1;
            
            current+=String.fromCharCode(asciiCode);
        }

        message = current;
        encodeArea.value = '';
        decodeArea.value = message;
        console.log('send');
    }

    function decode(e) {
        let current='';
        for (let index = 0; index < message.length; index++) {
            let asciiCode = message.charCodeAt(index)-1;
            
            current+=String.fromCharCode(asciiCode);
        }

        decodeArea.value = current;
    }
}