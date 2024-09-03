import react, { useEffect, useState } from 'react';
import { Col, Row } from 'react-bootstrap';
import noImageAvailable from '../noImageAvailable.jpg';
import { Link, useParams  } from 'react-router-dom';

const BookDetails = (props) => {
        const [book, setBook] = useState(null);

        const {bookid} = useParams();

        useEffect(() =>{
            fetch(process.env.REACT_APP_API_URL + "/book/" + bookid)
            .then(res => res.json())
            .then(res => {
                if(res.status === true){
                    setBook(res.data);
                }
            })
            .catch(err => alert("error in getting data"));
        
        }, []);


    return (
        <>
        <Row>
            {book &&
            <>
            <Col item xs={12} md={4}>
            <img src={book.coverImage || noImageAvailable} style={{width: 300, height:300}}/>
            </Col>
            <Col item xs={12} md={8}>
            <h3>{book.title}</h3>
            <p>{book.description || 'N/A'}</p>
            <div><b>Language:</b></div>
            <div>{book.language}</div>
            <div><b>Published</b></div>
            <div>{book.releaseImage && book.releaseImage.split('T')[0]}</div>
            <div><b>Writer:</b></div>
            <div>{book.writers.map(x => x.name).join(", ")}</div>
            </Col>
            <Col>
            <Link to="/">Go to Home page</Link>
            </Col>
            </>
            
            }
        </Row>
        </>
    )
}

export default BookDetails;