import react, { useState } from 'react';
import { Row, Col, Button } from 'react-bootstrap';
import BookList from '../components/book-list';
import CreateBookModel from '../components/create-book-model';


const Landing = () => {
            const[show, setShow] = useState(false);

    


    return (
        <>
        <Row>
            <Col xs={12} md={10}>
            <h2>Books</h2>
            </Col>
            <Col xs={12} md={2} className='align-self-center'>
            <Button className='float-right' onClick={() => setShow(true)}>Add New Book</Button>
            </Col>
        </Row>

        <BookList/>
        <CreateBookModel show={show} handleClose={() => setShow(false)} />
        </>
    )
}

export default Landing;