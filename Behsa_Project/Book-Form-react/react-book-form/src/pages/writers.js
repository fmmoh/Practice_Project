import react from 'react';
import { useNavigate } from 'react-router-dom';
import { Row, Col, Button } from 'react-bootstrap';
import WriterList from '../components/writer-list';
const Writers = (props) => {
    const navigate = useNavigate();
    return (
        <>
        <Row>
        <Col xs={12} md={10}>
            <h2>Writers</h2>
            </Col>
            <Col xs={12} md={2} className='align-self-center'>
            <Button className='float-right' onClick={() => navigate('/writers/Create-edit')}>Add New Writer</Button>
            </Col>
        </Row>

        <WriterList/>

        </>
    )
}

export default Writers;