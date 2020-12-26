function attachEvents() {
    const btnViewPostElement = document.getElementById('btnViewPost');
    const postsSelectElement = document.getElementById('posts');
    const btnLoadPostsElement = document.getElementById('btnLoadPosts');

    const postTitleElement = document.getElementById('post-title');
    const postCommentsElement = document.getElementById('post-comments');
    const postBodyElement = document.getElementById('post-body');

    const url = `https://blog-apps-c12bf.firebaseio.com/.json`;

    btnLoadPostsElement.addEventListener('click',function(){
        fetch(url)
        .then(response=> response.json())
        .then(data=>{
            let titles = Array.from(Object.keys(data.posts)).map(p=> `<option value="${p}">${data.posts[p].title}</option>`);

            postsSelectElement.innerHTML = titles.join('');
            
            btnViewPostElement.addEventListener('click',function(){
                let urlPost = `https://blog-apps-c12bf.firebaseio.com/posts/${postsSelectElement.value}.json`;
                
                fetch(urlPost)
                    .then(response=> response.json())
                    .then(data=>{
                        console.log(data);
                        postTitleElement.textContent = data.title;
                        postBodyElement.innerHTML = data.body;
                        postCommentsElement.innerHTML = Array.from(Object.keys(data['-MLcsjEabX-RZp_xqAj6'])).map(k=> `<li>${data['-MLcsjEabX-RZp_xqAj6'][k]}</li>`).join('');
                    })
            })
        })
    })
}

attachEvents();