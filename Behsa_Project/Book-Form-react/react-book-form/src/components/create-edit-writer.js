import React, { useEffect, useState } from 'react';
import { Form , Button } from 'react-bootstrap'; 
import { useLocation, useNavigate } from 'react-router-dom';

const CreateEditWriter = (props) => {

    const[validated, setValidated] = useState(false);
    const[writer, setWriter] =useState({
        id:0,
        name: '',
        dateOfBirth: undefined
    });

    const location = useLocation();
    const navigate = useNavigate(); 

    useEffect(() =>{
        if(location.state && location.state.data && location.state.data.id > 0){
            let writerData = location.state.data;
            if(writerData.dateOfBirth !== null && writerData.dateOfBirth !== undefined){
                writerData.dateOfBirth = writerData.dateOfBirth.split('T')[0]; 
            }
        setWriter(writerData);
        }
        else{
            setWriter({
                id: 0,
                name: '',
                dateOfBirth: ''
            })
        }
    }, [location.state]);

    const handleFieldChange = (event) => {
        var da = writer;
            da[event.target.name] = event.target.value;

        setWriter(oldData => {return {...oldData, ...da};});
    }


    const handleSave = (event) => {
        event.preventDefault();
        const form = event.currentTarget;
        if(form.checkValidity() === false){
            event.stopPropagation();
            setValidated(true);
            return;
        }


        if(writer && writer.id > 0){
            //update
            fetch(process.env.REACT_APP_API_URL + "/writer", {
                method: "PUT",
                headers: {
                    'Accept': 'application/json',
                    'content-Type': 'application/json',
                },
                body: JSON.stringify(writer)  
                })
                .then(res => res.json())
                .then(res => {
                        if(res.status === true && res.data){
                            let writerData = res.data;
                            if(writerData.dateOfBirth !== null && writerData.dateOfBirth !== undefined){
                                writerData.dateOfBirth = writerData.dateOfBirth.split('T')[0]; 
                            }
                        setWriter(writerData);
                        alert('update data successfully.')
                    }
        })
        .catch(err => alert("Error in getting data"));
    }
    else{
        //create
        fetch(process.env.REACT_APP_API_URL + "/writer", {
            method: "POST",
            headers: {
                'Accept': 'application/json',
                'content-Type': 'application/json',
            },
            body: JSON.stringify(writer)  
            })
            .then(res => res.json())
            .then(res => {
                    if(res.status === true && res.data){
                        let writerData = res.data;
                        if(writerData.dateOfBirth !== null && writerData.dateOfBirth !== undefined){
                            writerData.dateOfBirth = writerData.dateOfBirth.split('T')[0];
                        }
                    setWriter(writerData);
                    alert('Created successfully.');
                    navigate('/writers');
                }
    })
    .catch(err => alert("Error in getting data"));
    }
        }

    return (
        <>    
        <Form noValidate validated={validated} onSubmit={handleSave}>
            <Form.Group controlId="formwriterName">
                <Form.Label>Name</Form.Label>
                <Form.Control name="name" value={writer.name || ''} required type ="text" autoComplete="off" placeholder="Enter Name" onChange={handleFieldChange} />
                <Form.Control.Feedback type="invalid">
                            Please enter name.
                </Form.Control.Feedback>
            </Form.Group>
            <Form.Group controlId="formwriterDateOfBirth">
                <Form.Label>Date of Birth</Form.Label>
                <Form.Control name="dateOfBirth" value={writer.dateOfBirth || ''} required type ="date"  onChange={handleFieldChange} />
                <Form.Control.Feedback type="invalid">
                            Please enter Date of Birth.
                </Form.Control.Feedback>
            </Form.Group>
            <Button type="submit">{writer && writer.id > 0 ? "Update" : "Create"}</Button>
        </Form>
</>
    )
}

export default CreateEditWriter;