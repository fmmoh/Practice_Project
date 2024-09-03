import React, { useEffect, useState } from 'react';
import { Form, Image, Button } from 'react-bootstrap';
import AsyncSelect from 'react-select/async';
import noImageAvailable from '../noImageAvailable.jpg';
import { useParams } from 'react-router-dom'; // Import useParams

const EditBook = () => {
    const { bookid } = useParams(); // Use useParams to get bookid from URL
    const [book, setBook] = useState({});
    const [writers, setWriters] = useState([]);
    const [validated, setValidated] = useState(false);

    useEffect(() => {
        if (bookid) {
            fetch(`${process.env.REACT_APP_API_URL}/book/${bookid}`)
                .then(res => res.json())
                .then(res => {
                    if (res.status === true) {
                        let bookData = res.data;
                        if (bookData.releaseImage !== null && bookData.releaseImage !== undefined) {
                            bookData.releaseImage = bookData.releaseImage.split('T')[0];
                        }
                        setBook(bookData);
                        setWriters(bookData.writers.map(x => { return { value: x.id, label: x.name } }));
                    }
                })
                .catch(err => alert("Error in getting data"));
        }
    }, [bookid]);

    const handleFileUpload = (event) => {
        event.preventDefault();
        var file = event.target.files[0];
        const form = new FormData();
        form.append("imageFile", file);

        fetch(`${process.env.REACT_APP_API_URL}/Book/upload-book-cover`, {
            method: "POST",
            body: form
        })
            .then(res => res.json())
            .then(res => {
                var da = book;
                da.coverImage = res.profileImage;
                setBook(oldData => { return { ...oldData, ...da }; });
            })
            .catch(err => alert("Error in file upload"));
    }

    const handleSave = (event) => {
        event.preventDefault();
        const form = event.currentTarget;
        if (form.checkValidity() === false) {
            event.stopPropagation();
            setValidated(true);
            return;
        }

        let bookToPost = { ...book };
        bookToPost.writers = bookToPost.writers.map(x => x.id);

        if (book && book.id) {
            // Update
            fetch(`${process.env.REACT_APP_API_URL}/book`, {
                method: "PUT",
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(bookToPost)
            })
                .then(res => res.json())
                .then(res => {
                    if (res.status === true && res.data) {
                        let bookData = res.data;
                        if (bookData.releaseImage !== null && bookData.releaseImage !== undefined) {
                            bookData.releaseImage = bookData.releaseImage.split('T')[0];
                        }
                        setBook(bookData);
                        alert('Update successful.');
                    }
                })
                .catch(err => alert("Error in updating data"));
        } else {
            // Create
            fetch(`${process.env.REACT_APP_API_URL}/book`, {
                method: "POST",
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(bookToPost)
            })
                .then(res => res.json())
                .then(res => {
                    if (res.status === true && res.data) {
                        let bookData = res.data;
                        if (bookData.releaseImage !== null && bookData.releaseImage !== undefined) {
                            bookData.releaseImage = bookData.releaseImage.split('T')[0];
                        }
                        setBook(bookData);
                        alert('Created successfully.');
                    }
                })
                .catch(err => alert("Error in creating data"));
        }
    }

    const handleFieldChange = (event) => {
        var da = book;
        da[event.target.name] = event.target.value;
        setBook(oldData => { return { ...oldData, ...da }; });
    }

    const promiseOptions = (inputValue) => {
        return fetch(`${process.env.REACT_APP_API_URL}/Writer/search/${inputValue}`)
            .then(res => res.json())
            .then(res => {
                if (res.status === true && res.data.length > 0) {
                    return res.data.map(x => { return { value: x.id, label: x.name } });
                }
                if (res.data.count === 0) {
                    alert("There is no writer matching this name.");
                }
            })
            .catch(err => alert("Error getting data"));
    }

    const multiselectchange = (data) => {
        setWriters(data);
        var people = data.map(x => { return { id: x.value, name: x.label } });
        var da = book;
        da.writers = people;
        setBook(oldData => { return { ...oldData, ...da }; });
    }

    return (
        <>
            <Form noValidate validated={validated} onSubmit={handleSave}>
                <Form.Group className="d-flex justify-content-center">
                    <Image width="200" height="200" src={book.coverImage || noImageAvailable} />
                </Form.Group>
                <Form.Group className="d-flex justify-content-center">
                    <div><input type='file' onChange={handleFileUpload} /></div>
                </Form.Group>
                <Form.Group controlId="formbookTitle">
                    <Form.Label>Book Title</Form.Label>
                    <Form.Control name="title" value={book.title || ''} required type="text" autoComplete="off" placeholder="Enter Book Name" onChange={handleFieldChange} />
                    <Form.Control.Feedback type="invalid">
                        Please enter book name.
                    </Form.Control.Feedback>
                </Form.Group>
                <Form.Group controlId="formbookDescription">
                    <Form.Label>Book Description</Form.Label>
                    <Form.Control name="description" value={book.description || ''} type="textarea" rows={3} placeholder="Enter Book description" onChange={handleFieldChange} />
                </Form.Group>
                <Form.Group controlId="formbookReleaseImage">
                    <Form.Label>Published</Form.Label>
                    <Form.Control name="releaseImage" value={book.releaseImage || ''} required type="date" onChange={handleFieldChange} />
                    <Form.Control.Feedback type="invalid">
                        Please enter book release date.
                    </Form.Control.Feedback>
                </Form.Group>
                <Form.Group controlId="formbookWriter">
                    <Form.Label>Writer</Form.Label>
                    <AsyncSelect cacheOptions isMulti value={writers} loadOptions={promiseOptions} onChange={multiselectchange} />
                </Form.Group>
                <Form.Group controlId="formbookLanguage">
                    <Form.Label>Book Language</Form.Label>
                    <Form.Control name="language" value={book.language || ''} type="text" placeholder="Enter Book Language" onChange={handleFieldChange} />
                </Form.Group>
                <Button type="submit">{book.id ? "Update" : "Create"}</Button>
            </Form>
        </>
    );
}

export default EditBook;
