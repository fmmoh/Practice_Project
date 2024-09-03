import React from 'react';
import { useNavigate } from 'react-router-dom';
import { Row, Col, Button } from 'react-bootstrap';
import noImageAvailable from '../noImageAvailable.jpg';


const BookItem = (props) => {
    const navigate = useNavigate(); 
    
    return (
        <>
                <Row>
                    <Col item xs={12} md={2}>
                    <img src={props.data.coverImage || noImageAvailable} style={{width:150, height:150}}/>
                    </Col>
                    <Col item xs={12} md={10}>
                    <div><b>{props.data.title}</b></div>
                    <div>Writers: {props.data.writers.map(x => x.name).join(", ")}</div>
                    <Button onClick={() => navigate('/details/' + props.data.id)}>See Details</Button>{' '}
                    <Button onClick={() => navigate('/edit/' + props.data.id)}>Edit</Button>{' '}
                    <Button variant="danger" onClick={() => props.deleteBook(props.data.id)} danger>Delete</Button>

                    </Col>
                    <Col>
                    <hr />
                    </Col>
                </Row>
        </>
    )
}

export default BookItem;