import react from 'react';
import { Modal } from 'react-bootstrap';
import EditBook from './edit-book';

const CreateBookModel = (props) => {



    return (
        <>
        <Modal show={props.show} onHide={props.handleClose} backdrop="static" keyboard={false} centered>
            <Modal.Header closeButton>
                <Modal.Title>Add New Book</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <EditBook />
            </Modal.Body>
        </Modal>
        </>
    )
}

export default CreateBookModel;