import React, { useState, useEffect } from 'react';
import { Row, Col, Button } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import ReactPaginate from 'react-paginate';

const WriterList = () => {
    const [writers, setWriters] = useState([]);
    const [writersCount, setWritersCount] = useState(0);
    const [page, setPage] = useState(0);

    const navigate = useNavigate();

    useEffect(() => {
        getWriter();
    }, [page]);

    const getWriter = () => {
        fetch(process.env.REACT_APP_API_URL + "/writer?pageSize=" + process.env.REACT_APP_PAGING_SIZE + "&pageIndex=" + page)
            .then(res => res.json())
            .then(res => {
                if (res.status === true && res.data.count > 0) {
                    setWriters(res.data.writer);
                    setWritersCount(Math.ceil(res.data.count / process.env.REACT_APP_PAGING_SIZE));
                } else if (res.data.count === 0) {
                    alert("There is no writer data in the system.");
                }
            })
            .catch(err => alert("Error getting data"));
    }

    const handlePageClick = (pageIndex) => {
        setPage(pageIndex.selected);
    }

    const deleteWriter = (id) => {
        fetch(process.env.REACT_APP_API_URL + "/writer?id=" + id, {
            method: "DELETE",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            }
        })
        .then(res => res.json())
        .then(res => {
            if (res.status === true) {
                alert(res.message);
                getWriter();
            }
        })
        .catch(err => alert("Error deleting data"));
    }

    return (
        <>
        {writers.length > 0 ?
        <div>
            {writers.map((writer) => (
                <Row key={writer.id}>
                    <Col>
                        <div onClick={() => navigate('/writers/details/' + writer.id)}>
                            <b><u>{writer.name}</u></b>
                        </div>
                        <Button onClick={() => navigate('/writers/Create-edit', { state: { data: writer } })}>Edit</Button>{' '}
                        <Button variant="danger" onClick={() => deleteWriter(writer.id)}>Delete</Button>
                        <hr />
                    </Col>
                </Row>
            ))}
        </div>
        : ""}

        <div className='d-flex justify-content-center'>
            <ReactPaginate
                previousLabel={'previous'}
                nextLabel={'next'}
                breakLabel={'...'}
                breakClassName={'page-link'}
                pageCount={writersCount}
                marginPagesDisplayed={2}
                pageRangeDisplayed={5}
                onPageChange={handlePageClick}
                containerClassName={'pagination'}
                pageClassName={'page-item'}
                pageLinkClassName={'page-link'}
                previousClassName={'page-link'}
                nextClassName={'page-link'}
                activeClassName={'active'}
            />
        </div>
        </>
    );
}

export default WriterList;
