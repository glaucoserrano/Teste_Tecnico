import { Alert, Box, Button, Container, Dialog, DialogActions, DialogContent, DialogTitle, Paper, Snackbar, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, TextField, Typography } from "@mui/material"
import axios from "axios"
import { useEffect, useState } from "react"
import { useForm } from "react-hook-form"

interface Usuario {
    id: number,
    nome: string,
    email: string
}
const Usuarios = () =>{
    const BASEURL = "https://localhost:7205/api/"
    const [usuarios,setUsuarios] = useState<Usuario[]>([])
    const [openModal,setOpenModal] = useState(false)
    const [openComfirm,setOpenConfirm] = useState(false)
    const [usuarioSelecionado, setUsuarioSelecionado]= useState<Usuario | null>(null)
    const [openSnackbar, setOpenSnackbar] = useState(false)
    const [mensagem, setMensagem] = useState("")

    const {register,
        handleSubmit,
        reset,
        formState:{errors}
    } = useForm<Usuario>()
    const fetchUsuarios = async () => {
        try{
            const response = await axios.get(`${BASEURL}Usuario`)
            setUsuarios(response.data)
        }catch(error){
            console.log(error)
        }
    }
    useEffect (() => {
        fetchUsuarios()
    }, [])

    const handleOpenModal = (usuario?: Usuario) =>{
        setUsuarioSelecionado(usuario || null)
        reset(usuario || {nome: "", email:""})
        setOpenModal(true)
    }
    const handleCloseModal = () => {
        setOpenModal(false)
        setUsuarioSelecionado(null)
    }
    const onSubmit = async(data:Usuario) => {
        try{
            if(usuarioSelecionado){
                await axios.put(`${BASEURL}Usuario/${usuarioSelecionado.id}`,data)
            }else{
                await axios.post(`${BASEURL}Usuario`, data)
            }
            setMensagem("Usuário cadastrado com sucesso!")
            setOpenSnackbar(true);
            fetchUsuarios()
            handleCloseModal()
        }catch(error : any){
            if(error.response && error.response.data){
                setMensagem(error.response.data.mensagem)
                console.log(error.response.data.mensagem)
            }else{
                console.log(error)
                setMensagem("Erro ao cadastrar usuario")
            }
            setOpenSnackbar(true)
        }
    }
    const handleOpenConfirm = (usuario: Usuario) => {
        setUsuarioSelecionado(usuario)
        setOpenConfirm(true)
    }
    const handleCloseConfirm = () => {
        setOpenConfirm(false)
        setUsuarioSelecionado(null)
    }

    const handleDelete = async () =>{
        if(usuarioSelecionado){
            try{
                await axios.delete(`${BASEURL}Usuario/${usuarioSelecionado.id}`)
                fetchUsuarios()
            }catch(error){
                console.log(error)
            }
            handleCloseConfirm();
        }
    }
    return (
        <Container maxWidth="lg">
            <Box sx= {{mt:4, mb: 2, display:"flex", justifyContent:"center", alignItems:"center"}}>
                <Typography variant="h5">Usuários Cadastrados </Typography>
                <Button style={{marginLeft:"10px"}} variant="contained" color="primary" onClick={() => handleOpenModal()}>
                    Adicionar Usuário
                </Button>
            </Box>
            <TableContainer component={Paper}>
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCell>Nome</TableCell>
                            <TableCell>E-mail</TableCell>
                            <TableCell>Ações</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {usuarios.map((usuario) =>(
                            <TableRow key={usuario.id}>
                                <TableCell>{usuario.nome}</TableCell>
                                <TableCell>{usuario.email}</TableCell>
                                <TableCell align="right">
                                    <Button color="primary" onClick={() => handleOpenModal(usuario)}>
                                        Editar
                                    </Button>
                                    <Button color="error" onClick={() => handleOpenConfirm(usuario)}>
                                        Excluir
                                    </Button>
                                </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
            <Dialog open={openModal} onClose={handleCloseModal}>
                <DialogTitle>{usuarioSelecionado ? "Editar Usuário" : "Adicionar Usuário"}</DialogTitle>
                <DialogContent>
                    <form onSubmit={handleSubmit(onSubmit)}>
                        <TextField
                            label="Nome"
                            fullWidth
                            margin="normal"
                            {...register("nome",{required: "Nome é obrigatório"})}
                            error={!!errors.nome}
                            helperText={errors.nome?.message}
                        />
                        <TextField
                            label="E-mail"
                            fullWidth
                            margin="normal"
                            {...register("email",{required: "E-mail é obrigatório"})}
                            error={!!errors.email}
                            helperText={errors.email?.message}
                        />
                        <DialogActions>
                            <Button onClick={handleCloseModal} color="secondary">
                                Cancelar
                            </Button>
                            <Button type="submit" variant="contained" color="primary">
                                Salvar
                            </Button>
                        </DialogActions>
                    </form>
                </DialogContent>
            </Dialog>
            <Dialog open={openComfirm} onClose={handleCloseConfirm}>
                <DialogTitle>Confirmar Exclusão</DialogTitle>
                <DialogContent>
                    <Typography>
                        Tem certeza que deseja excluir "{usuarioSelecionado?.nome}"?
                    </Typography>
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleCloseConfirm} color="secondary">
                        Cancelar
                    </Button>
                    <Button onClick={handleDelete} variant="contained" color="primary">
                        Excluir
                    </Button>
                </DialogActions>
            </Dialog>
            <Snackbar 
                open={openSnackbar} 
                autoHideDuration={3000} 
                onClose={() => setOpenSnackbar(false)}
            >
                <Alert
                    severity={mensagem.includes("sucesso") ? "success" : "error"}
                    onClose={() => setOpenSnackbar(false)}
                >
                    {mensagem}
                </Alert>
            </Snackbar>
        </Container>
    )
}
export default Usuarios