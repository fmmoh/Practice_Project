import React, { useState, useEffect } from 'react'; // Import useState and useEffect
import { Row, Col } from 'react-bootstrap'; // Import Col from react-bootstrap
import { useParams, Link } from 'react-router-dom';


const WriterDetail = (props) => {

    const [writer, setWriter] = useState(null);

        const {writerid} = useParams();

        useEffect(() =>{
            // console.log('Writer ID:', writerid);
            if (writerid) {
            fetch(process.env.REACT_APP_API_URL + "/writer/" + writerid)
            .then(res => res.json())
            .then(res => {
                if(res.status === true){
                    setWriter(res.data);
                }
            })
            .catch(err => alert("error in getting data"));
        } else {
            console.error('Invalid writer ID:', writerid);
        }
    }, [writerid]);


    return (
        <>
        <Row>
            {writer &&
            <>
            <Col item xs={12} md={12}>
            <h3>{writer.name}</h3>
            <div><b>Date of birth:</b></div>
            <div>{writer.dateOfBirth && writer.dateOfBirth.split('T')[0]}</div>
            <div><b>Books:</b></div>
            <ul>{writer.books.map(x => <li>{x}</li>)}</ul>
            </Col>
            <Col>
            <Link to="/writers">Go to Writers page</Link>
            </Col>
            </>
            
            }
        </Row>
        </>
    )
}

export default WriterDetail;